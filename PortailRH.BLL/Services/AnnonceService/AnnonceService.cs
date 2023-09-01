using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Annonce;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using PortailRH.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.AnnonceService
{
    /// <summary>
    /// AnnonceService
    /// </summary>
    public class AnnonceService : IAnnonceService
    {
        /// <summary>
        /// IGenericRepository<Annonce>
        /// </summary>
        private readonly IGenericRepository<Annonce> _annonceRepository;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// ILogger<AnnonceService>
        /// </summary>
        private readonly ILogger<AnnonceService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="annonceRepository">IGenericRepository<Annonce></param>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger<AnnonceService></param>
        public AnnonceService(IGenericRepository<Annonce> annonceRepository,
            IUnitOfWork unitOfWork, ILogger<AnnonceService> logger)
        {
            _annonceRepository = annonceRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get Annonces
        /// </summary>
        /// <param name="searchDto">AnnonceSearchDto</param>
        /// <returns>AnnoncePaginatedListDto</returns>
        public async Task<AnnoncePaginatedListDto> GetAnnonce(AnnonceSearchDto searchDto)
        {
            var annonces = await _annonceRepository.GetPaginatedListAsync(
                                                                    currentPage: searchDto.CurrentPage,
                                                                    itemsPerPage: searchDto.ItemsPerPage,
                                                                    predicate: x => (searchDto.Titre == null || x.Titre!.Contains(searchDto.Titre ?? string.Empty)) &&
                                                                                    (searchDto.DateAnnonce == null || x.DateAnnonce!.Value.Date == searchDto.DateAnnonce.Value.Date),
                                                                    orderBy: ord => ord.OrderByDescending(x => x.DateAnnonce)
                                                                );

            _logger.LogInformation("Get Annonces");

            return new AnnoncePaginatedListDto
            {
                Annonces = annonces.Entities.Select(x => new AnnonceDto
                {
                    Id = x.Id,
                    Titre = x.Titre,
                    Contenu = x.Contenu,
                    DateAnnonce = x.DateAnnonce
                }).ToArray(),
                Pagining = new PaginatedDto
                {
                    CurrentPage = annonces.CurrentPage,
                    ItemsPerPage = annonces.ItemsPerPage,
                    TotalItems = annonces.TotalItems,
                    TotalPages = annonces.TotalPages
                }
            };
        }

        /// <summary>
        /// Delete Annocne
        /// </summary>
        /// <param name="id">id annonce</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task DeleteAnnonce(int id)
        {
            var annonce = await _annonceRepository.GetAsync(
                                                        predicate: x => x.Id == id,
                                                        disableTracking: false
                                                    ) ?? throw new InvalidDataException($"Aucune annonce avec cette id {id}");

            _annonceRepository.Delete(annonce);

            await _unitOfWork.CommitAsync();
        }
    }
}
