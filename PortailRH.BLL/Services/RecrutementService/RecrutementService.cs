using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Recrutement;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.BLL.Mappers;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using PortailRH.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.RecrutementService
{
    /// <summary>
    /// RecrutementService
    /// </summary>
    public class RecrutementService : IRecrutementService
    {
        /// <summary>
        /// IGenericRepository<Recrutement>
        /// </summary>
        private readonly IGenericRepository<Recrutement> _recrutementRepository;

        /// <summary>
        /// IGenericRepository<TypeDocument>
        /// </summary>
        private readonly IGenericRepository<TypeDocument> _typeDocumentRepository;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// ILogger<RecrutementService>
        /// </summary>
        private readonly ILogger<RecrutementService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recrutementRepository">IGenericRepository<Recrutement></param>
        /// <param name="typeDocumentRepository">IGenericRepository<TypeDocument></param>
        /// <param name="unitOfWork">IUnitOfWork<Recrutement></param>
        /// <param name="logger">ILogger<RecrutementService></param>
        public RecrutementService(IGenericRepository<Recrutement> recrutementRepository, IGenericRepository<TypeDocument> typeDocumentRepository, IUnitOfWork unitOfWork, ILogger<RecrutementService> logger)
        {
            _recrutementRepository = recrutementRepository;
            _typeDocumentRepository = typeDocumentRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get list of recruitments
        /// </summary>
        /// <param name="searchDto">RecrutementSearchDto</param>
        /// <returns>RecrutementPaginatedListDto</returns>
        public async Task<RecrutementPaginatedListDto> GetRecrutementPaginatedList(RecrutementSearchDto searchDto)
        {
            var lstRecrutements = await _recrutementRepository.GetPaginatedListAsync(
                                                                                     currentPage: searchDto.CurrentPage,
                                                                                     itemsPerPage: searchDto.ItemsPerPage,
                                                                                     predicate: x => string.Concat(x.Prenom, " ", x.Nom).Contains(searchDto.NomComplet ?? string.Empty) &&
                                                                                                (searchDto.Departement == null || x.IdFonctionNavigation.IdDepartement == searchDto.Departement) &&
                                                                                                (searchDto.Fonction == null || x.IdFonction == searchDto.Fonction) &&
                                                                                                (searchDto.DateDebut == null || x.DatedCreation >= searchDto.DateDebut) &&
                                                                                                (searchDto.DateFin == null || x.DatedCreation <= searchDto.DateFin),
                                                                                     include: inc => inc.Include(x => x.IdFonctionNavigation).ThenInclude(x => x.IdDepartementNavigation)
                                                                                                        .Include(x => x.IdDocumentNavigation)
                                                                                );

            return new RecrutementPaginatedListDto
            {
                Recrutements = lstRecrutements.Entities.Select(x => x.ToGetRecrutementDto()).ToList(),
                Pagining = new PaginatedDto
                {
                    CurrentPage = lstRecrutements.CurrentPage,
                    ItemsPerPage = lstRecrutements.ItemsPerPage,
                    TotalItems = lstRecrutements.TotalItems,
                    TotalPages = lstRecrutements.TotalPages
                }
            };
        }

        /// <summary>
        /// Delete Recrutement
        /// </summary>
        /// <param name="id">id recrutement</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task DeleteRecrutement(int id)
        {
            var recrutement = await _recrutementRepository.GetAsync(
                                                                    predicate: x => x.Id == id,
                                                                    disableTracking: false
                                                                )
                                ?? throw new InvalidDataException($"Aucun recrutement avec cet identifiant (ID) {id}");

            _recrutementRepository.Delete(recrutement);

            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Get Details Recrutement
        /// </summary>
        /// <param name="id">id recrutement</param>
        /// <returns>GetRecrutementDto</returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task<GetRecrutementDto> GetDetialsRecrutement(int id)
        {
            var recrutement = await _recrutementRepository.GetAsync(
                                                                    predicate: x => x.Id == id,
                                                                    include: inc => inc.Include(x => x.IdFonctionNavigation).ThenInclude(x => x.IdDepartementNavigation)
                                                                                        .Include(x => x.IdDocumentNavigation)
                                                                )
                                ?? throw new InvalidDataException($"Aucun recrutement avec cet identifiant (ID) {id}");

            return recrutement.ToGetRecrutementDto();
        }

        /// <summary>
        /// Added Recrutement
        /// </summary>
        /// <param name="recrutementDto">NouveauxRecrutementDto</param>
        /// <returns></returns>
        public async Task AddNewRecrutement(NouveauxRecrutementDto recrutementDto)
        {
            var idTypeDocument = (await _typeDocumentRepository.GetAsync(x => x.Nom!.ToUpper() == "CV"))?.Id;
            await _recrutementRepository.AddedAsync(new Recrutement
            {
                Nom = recrutementDto.Nom,
                Prenom = recrutementDto.Prenom,
                DatedCreation = recrutementDto.DateCreation,
                Email = recrutementDto.Email,
                Telephone = recrutementDto.Telephone,
                IdFonction = recrutementDto.Fonction,
                IdDocumentNavigation = new Document
                {
                    Nom = recrutementDto.NomDocument,
                    IdTypeDocument = idTypeDocument ?? 0
                }
            });

            await _unitOfWork.CommitAsync();
        }
    }
}
