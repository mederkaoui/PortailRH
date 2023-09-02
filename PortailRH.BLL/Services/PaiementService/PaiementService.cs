using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Paiement;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using PortailRH.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.PaiementService
{
    /// <summary>
    /// PaiementService
    /// </summary>
    public class PaiementService : IPaiementService
    {
        /// <summary>
        /// IGenericRepository<Paiement>
        /// </summary>
        private readonly IGenericRepository<Paiement> _paiementRepository;

        /// <summary>
        /// IGenericRepository<Employe>
        /// </summary>
        private readonly IGenericRepository<Employe> _employeRepository;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// ILogger<PaiementService>
        /// </summary>
        private readonly ILogger<PaiementService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paiementRepository">IGenericRepository<Paiement></param>
        /// <param name="employeRepository">IGenericRepository<Employe></param>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger<PaiementService></param>
        public PaiementService(IGenericRepository<Paiement> paiementRepository, IGenericRepository<Employe> employeRepository,
            IUnitOfWork unitOfWork, ILogger<PaiementService> logger)
        {
            _paiementRepository = paiementRepository;
            _employeRepository = employeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get Payments
        /// </summary>
        /// <param name="searchDto">PaiementSearchDto</param>
        /// <returns>PaiementPaginatedListDto</returns>
        public async Task<PaiementPaginatedListDto> GetPayments(PaiementSearchDto searchDto)
        {
            //Get Payments
            var payments = await _paiementRepository.GetPaginatedListAsync(
                                                                        currentPage: searchDto.CurrentPage,
                                                                        itemsPerPage: searchDto.ItemsPerPage,
                                                                        predicate: x => x.DatePaiement!.Value.Month == searchDto.Mois &&
                                                                                        x.DatePaiement!.Value.Year == searchDto.Annee,
                                                                        include: inc => inc.Include(x => x.CinEmployeNavigation)
                                                                    );

            //Get Employes
            var employes = await _employeRepository.GetListAsync(
                                                    predicate: x => x.EstSupprime == false &&
                                                                (searchDto.SearchQuery == null || (x.Cin.Contains(searchDto.SearchQuery) || string.Concat(x.Prenom, " ", x.Nom).Contains(searchDto.SearchQuery)))
                                                );

            var result = new PaiementPaginatedListDto
            {
                Paiements = employes.Select(x => new PaiementDto
                {
                    Id = payments.Entities.FirstOrDefault(p => p.CinEmploye == x.Cin)?.Id,
                    CIN = x.Cin,
                    Nom = x.Nom,
                    Prenom = x.Prenom,
                    Salaire = Convert.ToSingle(payments.Entities.FirstOrDefault(p => p.CinEmploye == x.Cin)?.Salaire),
                    DatePaiement = payments.Entities.FirstOrDefault(p => p.CinEmploye == x.Cin)?.DatePaiement,
                    Statut = payments.Entities.FirstOrDefault(p => p.CinEmploye == x.Cin)?.Id != null ? "Payé" : "Non payé"
                }).ToList()
            };

            result.Pagining = new PaginatedDto
            {
                CurrentPage = searchDto.CurrentPage,
                ItemsPerPage = searchDto.ItemsPerPage,
                TotalItems = employes.Count(),
                TotalPages = Convert.ToInt32(employes.Count() / searchDto.ItemsPerPage)
            };

            return result;
        }

        /// <summary>
        /// Add new payment
        /// </summary>
        /// <param name="month">month of the payment</param>
        /// <param name="year">year of the payment</param>
        /// <returns></returns>
        public async Task AddNewPayment(int month, int year)
        {
            _logger.LogInformation("Get Employes");

            //Get Employes
            var employes = await _employeRepository.GetListAsync(
                                                    predicate: x => x.EstSupprime == false,
                                                    include: inc => inc.Include(x => x.Contrats)
                                                );


            var newPayment = employes.Select(x => new Paiement
            {
                CinEmploye = x.Cin,
                Salaire = x.Contrats.OrderByDescending(x => x.DateDebut).FirstOrDefault()?.Salaire,
                DatePaiement = new DateTime(year, month, DateTime.Now.Day)
            }).ToList();

            await _paiementRepository.AddedAsync(newPayment);

            _logger.LogInformation($"Add new payment for {month} {year}");

            await _unitOfWork.CommitAsync();
        }
    }
}
