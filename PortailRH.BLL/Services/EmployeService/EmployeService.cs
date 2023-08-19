using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Employe;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using PortailRH.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.EmployeService
{
    /// <summary>
    /// EmployeService
    /// </summary>
    public class EmployeService : IEmployeService
    {
        /// <summary>
        /// IGenericRepository<EmployeService>
        /// </summary>
        private readonly IGenericRepository<Employe> _employeRepository;

        /// <summary>
        /// IGenericRepository<Pay>
        /// </summary>
        private readonly IGenericRepository<Pay> _paysRepository;

        /// <summary>
        /// IGenericRepository<TypeContrat>
        /// </summary>
        private readonly IGenericRepository<TypeContrat> _typeContratRepository;

        /// <summary>
        /// IGenericRepository<Departement>
        /// </summary>
        private readonly IGenericRepository<Departement> _departementRepository;

        /// <summary>
        /// IGenericRepository<Banque>
        /// </summary>
        private readonly IGenericRepository<Banque> _banqueRepository;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// ILogger<EmployeService>
        /// </summary>
        private readonly ILogger<EmployeService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeRepository">IGenericRepository<EmployeService></param>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger<EmployeService></param>
        public EmployeService(IGenericRepository<Employe> employeRepository, IGenericRepository<Pay> paysRepository, IGenericRepository<TypeContrat> typeContratRepository,
            IGenericRepository<Departement> departementRepository, IGenericRepository<Banque> banqueRepository, IUnitOfWork unitOfWork, ILogger<EmployeService> logger)
        {
            _employeRepository = employeRepository;
            _paysRepository = paysRepository;
            _typeContratRepository = typeContratRepository;
            _departementRepository = departementRepository;
            _banqueRepository = banqueRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get Employe Select Lists Data
        /// </summary>
        /// <returns>EmployeSelectListsDto</returns>
        public async Task<EmployeSelectListsDto> GetSelectListsData()
        {
            //Get Countries with their cities
            var pays = await _paysRepository.GetListAsync(include: inc => inc.Include(x => x.Villes));

            //Get banks
            var banques = await _banqueRepository.GetListAsync();

            //Get departments with their functions
            var departements = await _departementRepository.GetListAsync(include: inc => inc.Include(x => x.Fonctions));

            //Get Types Contract
            var typesContrat = await _typeContratRepository.GetListAsync();

            _logger.LogInformation("Get Employe Select Lists Data");

            return new EmployeSelectListsDto
            {
                Pays = pays.Select(x => new PaysDto
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    Villes = x.Villes.Select(v => new VilleDto
                    {
                        Id = v.Id,
                        Nom = v.Nom
                    }).ToList(),
                }).ToList(),
                Departements = departements.Select(x => new DepartementDto
                {
                    Id = x.Id,
                    Nom = x.Nom,
                    Fonctions = x.Fonctions.Select(f => new FonctionDto
                    {
                        Id = f.Id,
                        Nom = f.Nom
                    }).ToList()
                }).ToList(),
                Banques = banques.Select(x => new BanqueDto
                {
                    Id = x.Id,
                    Nom = x.Nom,
                }).ToList(),
                TypesContrat = typesContrat.Select(x => new TypeContrat
                {
                    Id = x.Id,
                    Nom = x.Nom,
                }).ToList()
            };
        }


        /// <summary>
        /// Added new employe to DB
        /// </summary>
        /// <param name="employe">NouvelEmployeDto</param>
        /// <returns></returns>
        public async Task AddedEmploye(NouvelEmployeDto employe)
        {
            await _employeRepository.AddedAsync(new Employe
            {
                Cin = employe.CIN,
                Nom = employe.Nom,
                Prenom = employe.Prenom,
                Email = employe.Email,
                DateNaissance = employe.DateNaissance,
                Sexe = employe.Sexe,
                Telephone = employe.Telephone,
                SituationFamiliale = employe.SituationFamiliale,
                MatriculeCnss = employe.MatriculeCnss,
                NombreEnfants = employe.Enfants,
                Photo = employe.PhotoName,
                IdVille = employe.Ville,
                Adresse = employe.Adresse,
                IdFonction = employe.Fonction,
                IdBanque = employe.Banque,
                Rib = employe.RIB,
                ContactUrgenceNom = employe.ContactUrgenceNomComplet,
                ContactUrgenceTelephone = employe.ContactUrgenceTelephone,
                Contrats = new List<Contrat>
                {
                    new Contrat
                    {
                        IdType = employe.TypeContrat,
                        DateDebut = employe.DateEntree,
                        DateFin = employe.DateSortie,
                        Salaire = employe.Salaire
                    }
                }
            });

            await _unitOfWork.CommitAsync();
        }
    }
}
