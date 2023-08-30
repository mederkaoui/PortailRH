using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.DemandeDocument;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using PortailRH.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.DemandeDocumentService
{
    /// <summary>
    /// DocumentService
    /// </summary>
    public class DemandeDocumentService : IDemandeDocumentService
    {
        /// <summary>
        /// IGenericRepository<Document>
        /// </summary>
        private readonly IGenericRepository<DemandeDocument> _demandeDocumentRepository;

        /// <summary>
        /// IGenericRepository<TypeDocument>
        /// </summary>
        private readonly IGenericRepository<TypeDocument> _typeDocumentRepository;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// ILogger<DocumentService>
        /// </summary>
        private readonly ILogger<DemandeDocumentService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="demandeDocumentRepository">IGenericRepository<DemandeDocument></param>
        /// <param name="typeDocumentRepository">IGenericRepository<TypeDocument></param>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger<DocumentService></param>
        public DemandeDocumentService(IGenericRepository<DemandeDocument> demandeDocumentRepository,
            IGenericRepository<TypeDocument> typeDocumentRepository, IUnitOfWork unitOfWork, ILogger<DemandeDocumentService> logger)
        {
            _demandeDocumentRepository = demandeDocumentRepository;
            _typeDocumentRepository = typeDocumentRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get Paginated List Of Requested Documents
        /// </summary>
        /// <returns>DemandeDocumentPaginatedListDto</returns>
        public async Task<DemandeDocumentPaginatedListDto> GetRequestedDocumnet(DemandeDocumentSearchDto searchDto)
        {
            var paginatedDemandes = await _demandeDocumentRepository.GetPaginatedListAsync(
                                                                        currentPage: searchDto.CurrentPage,
                                                                        itemsPerPage: searchDto.ItemsPerPage,
                                                                        predicate: x => (searchDto.Statut == null || x.Statut == searchDto.Statut.ToString()) &&
                                                                                        (x.CinEmploye.Contains(searchDto.SearchQuery ?? string.Empty) || string.Concat(x.CinEmployeNavigation.Prenom, " " ,x.CinEmployeNavigation.Nom).Contains(searchDto.SearchQuery ?? string.Empty)) &&
                                                                                        (searchDto.DateDemande == null || x.DateDemande == searchDto.DateDemande),
                                                                        include: inc => inc.Include(x => x.CinEmployeNavigation)
                                                                                            .Include(x => x.IdDocumentNavigation),
                                                                        orderBy: ord => ord.OrderBy(x => x.DateDemande)
                                                                    );

            return new DemandeDocumentPaginatedListDto
            {
                Demandes = paginatedDemandes.Entities.Select(x => new DemandeDocumentDto
                {
                    Id = x.Id,
                    CIN = x.CinEmploye,
                    Nom = x.CinEmployeNavigation.Nom,
                    Prenom = x.CinEmployeNavigation.Prenom,
                    DateDemande = x.DateDemande,
                    Raison = x.Raison,
                    Status = x.Statut == DemandeDocumentStatusEnum.EnAttente.ToString() ? "En Attente" : 
                                        (
                                            x.Statut == DemandeDocumentStatusEnum.EnCours.ToString() ? "En Cours" : 
                                            (
                                                x.Statut == DemandeDocumentStatusEnum.Termine.ToString() ? DemandeDocumentStatusEnum.Termine.ToString() : 
                                                DemandeDocumentStatusEnum.Annule.ToString()
                                            )
                                        ),
                }).ToList(),
                Pagining = new PaginatedDto
                {
                    CurrentPage = paginatedDemandes.CurrentPage,
                    ItemsPerPage = paginatedDemandes.ItemsPerPage,
                    TotalItems = paginatedDemandes.TotalItems,
                    TotalPages = paginatedDemandes.TotalPages
                }
            };
        }

        /// <summary>
        /// Get Details Request Document
        /// </summary>
        /// <param name="id">id demande</param>
        /// <returns>DemandeDocumentDto</returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task<DemandeDocumentDto> GetDetailsDemande(int id)
        {
            var demande = await _demandeDocumentRepository.GetAsync(
                                                                    predicate: x => x.Id == id,
                                                                    include: inc => inc.Include(x => x.CinEmployeNavigation)
                                                                                        .Include(x => x.IdDocumentNavigation)
                                                                                        .ThenInclude(x => x.IdTypeDocumentNavigation)
                                                                ) ?? throw new InvalidDataException($"Aucune demande avec cette id: {id}");

            _logger.LogInformation("Get Details Request Document");

            return new DemandeDocumentDto
            {
                Id = demande.Id,
                CIN = demande.CinEmploye,
                Nom = demande.CinEmployeNavigation.Nom,
                Prenom = demande.CinEmployeNavigation.Prenom,
                DateDemande = demande.DateDemande,
                Raison = demande.Raison,
                TitreDocument = demande.TitreDocument,
                DocumentNom = demande.IdDocumentNavigation?.Nom,
                TypeDocument = demande.IdDocumentNavigation?.IdTypeDocumentNavigation.Nom,
                Status = demande.Statut == DemandeDocumentStatusEnum.EnAttente.ToString() ? "En Attente" :
                                        (
                                            demande.Statut == DemandeDocumentStatusEnum.EnCours.ToString() ? "En Cours" :
                                            (
                                                demande.Statut == DemandeDocumentStatusEnum.Termine.ToString() ? DemandeDocumentStatusEnum.Termine.ToString() :
                                                DemandeDocumentStatusEnum.Annule.ToString()
                                            )
                                        ),
            };
        }

        /// <summary>
        /// Get Types Document
        /// </summary>
        /// <returns>ICollection<TypeDocumentDto/></returns>
        public async Task<ICollection<TypeDocumentDto>> GetTypesDocument()
        {
            var types = await _typeDocumentRepository.GetListAsync();

            return types.Select(x => new TypeDocumentDto
            {
                Id = x.Id,
                Nom = x.Nom
            }).ToList();
        }

        /// <summary>
        /// Added Document to the domande
        /// </summary>
        /// <param name="uploadDocumentDto">UploadDocumentDto</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task AddedDocument(UploadDocumentDto uploadDocumentDto)
        {
            var demande = await _demandeDocumentRepository.GetAsync(
                                                                    predicate: x => x.Id == uploadDocumentDto.IdDemande,
                                                                    disableTracking: false
                                                                ) ?? throw new InvalidDataException($"Aucune demande avec cette id: {uploadDocumentDto.IdDemande}");

            var type = await _typeDocumentRepository.GetAsync(x => x.Id == uploadDocumentDto.IdTypeDocument)
                                                    ?? throw new InvalidDataException($"Aucune type de document avec l'id: {uploadDocumentDto.IdTypeDocument}");

            demande.Statut = DemandeDocumentStatusEnum.Termine.ToString();
            demande.IdDocumentNavigation = new Document
            {
                Nom = uploadDocumentDto.DocumentNom,
                IdTypeDocument = type.Id
            };

            _demandeDocumentRepository.Update(demande);

            await _unitOfWork.CommitAsync();
        }
    }
}
