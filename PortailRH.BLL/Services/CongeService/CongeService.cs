using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Conge;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using PortailRH.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.CongeService
{
	/// <summary>
	/// CongeService
	/// </summary>
	public class CongeService : ICongeService
	{
		/// <summary>
		/// IGenericRepository<Conge>
		/// </summary>
		private readonly IGenericRepository<Conge> _congeRepository;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

		/// <summary>
		/// ILogger<CongeService>
		/// </summary>
		private readonly ILogger<CongeService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="congeRepository">IGenericRepository<Conge></param>
        /// <param name="IUnitOfWork">unitOfWork<Conge></param>
        /// <param name="logger">ILogger<CongeService></param>
        public CongeService(IGenericRepository<Conge> congeRepository, IUnitOfWork unitOfWork, ILogger<CongeService> logger)
        {
            _congeRepository = congeRepository;
			_unitOfWork = unitOfWork;
			_logger = logger;
        }

		/// <summary>
		/// Get Conges with employe full name
		/// </summary>
		/// <returns>ICollection<CongeDto></returns>
		public async Task<ICollection<CongeDto>> GetConges()
		{
			var conges = await _congeRepository.GetListAsync(
													predicate: x => x.Statut != null && x.Statut == CongeStatusEnum.Accepter.ToString(),
													include: inc => inc.Include(x => x.CinEmployeNavigation)
												);

			return conges.Select(x => new CongeDto
			{
				Id = x.Id,
				DateDebut = x.DateDebut,
				DateFin	= x.DateFin,
				Status = x.Statut,
				EmployeNomComplet = string.Concat(x.CinEmployeNavigation.Nom, " ", x.CinEmployeNavigation.Prenom)
			}).ToList();
		}

        /// <summary>
        /// Get All Conges
        /// </summary>
        /// <param name="searchDto">DemandesSearchDto</param>
        /// <returns>DemandesCongesPaginatedListDto</returns>
		public async Task<DemandesCongesPaginatedListDto> GetAllConges(DemandesSearchDto searchDto)
		{
            var conges = await _congeRepository.GetPaginatedListAsync(
                                                            currentPage: searchDto.CurrentPage,
                                                            itemsPerPage: searchDto.ItemsPerPage,
                                                            predicate: x => (x.CinEmploye.Contains(searchDto.SearchQuery ?? string.Empty) || string.Concat(x.CinEmployeNavigation.Prenom, " ", x.CinEmployeNavigation.Nom).Contains(searchDto.SearchQuery ?? string.Empty)),
                                                            include: inc => inc.Include(x => x.CinEmployeNavigation),
                                                            orderBy: x => x.OrderByDescending(x => x.DateDebut)
                                                        );

            return new DemandesCongesPaginatedListDto
            {
                Demandes = conges.Entities.Select(x => new DemandesCongesDto
                {
                    Id = x.Id,
                    DateDebut = x.DateDebut,
                    DateFin = x.DateFin,
                    Status = x.Statut,
                    CIN = x.CinEmploye,
                    Nom = x.CinEmployeNavigation.Nom,
                    Prenom = x.CinEmployeNavigation.Prenom
                }).ToList(),
                Pagining = new PaginatedDto
                {
                    CurrentPage = searchDto.CurrentPage,
                    ItemsPerPage = searchDto.ItemsPerPage,
                    TotalItems = conges.TotalItems,
                    TotalPages = conges.TotalPages
                }
            };
        }

        /// <summary>
        /// Retrieve "Conges" with a status other than "accept".
        /// </summary>
        /// <returns>ICollection<DemandesCongesDto></returns>
        public async Task<DemandesCongesPaginatedListDto> GetCongesRequests(DemandesSearchDto searchDto)
		{
			var conges = await _congeRepository.GetPaginatedListAsync(
															currentPage: searchDto.CurrentPage,
															itemsPerPage: searchDto.ItemsPerPage,
															predicate: x => x.Statut != null && x.Statut == CongeStatusEnum.OnAttends.ToString() &&
                                                                            (x.CinEmploye.Contains(searchDto.SearchQuery ?? string.Empty) || string.Concat(x.CinEmployeNavigation.Prenom, " ", x.CinEmployeNavigation.Nom).Contains(searchDto.SearchQuery ?? string.Empty)),
                                                            include: inc => inc.Include(x => x.CinEmployeNavigation),
                                                            orderBy: x => x.OrderByDescending(x => x.DateDebut)
                                                        );

			return new DemandesCongesPaginatedListDto
			{
				Demandes = conges.Entities.Select(x => new DemandesCongesDto
                {
                    Id = x.Id,
                    DateDebut = x.DateDebut,
                    DateFin = x.DateFin,
                    Status = x.Statut,
                    CIN = x.CinEmploye,
                    Nom = x.CinEmployeNavigation.Nom,
                    Prenom = x.CinEmployeNavigation.Prenom
                }).ToList(),
				Pagining = new PaginatedDto
				{
					CurrentPage = searchDto.CurrentPage,
					ItemsPerPage = searchDto.ItemsPerPage,
					TotalItems = conges.TotalItems,
					TotalPages = conges.TotalPages
				}
			};
		}

        /// <summary>
        /// Updqte Conge Status
        /// </summary>
        /// <param name="id">id conge</param>
        /// <param name="isAccepted">status conge</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task UpdateCongeStatus(UpdateCongeStatusDto updateCongeStatusDto)
		{
			var conge = await _congeRepository.GetAsync(
														predicate: x => x.Id == updateCongeStatusDto.Id,
														disableTracking: false
													)
							?? throw new InvalidDataException($"Il n'y a pas de congé avec cet identifiant: {updateCongeStatusDto.Id}");

			conge.Statut = updateCongeStatusDto.Status.ToString();

			_congeRepository.Update(conge);

			await _unitOfWork.CommitAsync();
		}

        /// <summary>
        /// Delete Conge
        /// </summary>
        /// <param name="id">id conge</param>
        /// <returns></returns>
        /// <exception cref="InvalidDataException">InvalidDataException</exception>
        public async Task DeleteConge(int id)
		{
            var conge = await _congeRepository.GetAsync(
                                                        predicate: x => x.Id == id,
                                                        disableTracking: false
                                                    )
                            ?? throw new InvalidDataException($"Il n'y a pas de congé avec cet identifiant: {id}");

			_congeRepository.Delete(conge);

			await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Add Conge
        /// </summary>
        /// <param name="congeDto">AddCongeDto</param>
        /// <returns></returns>
        public async Task AddConge(AddCongeDto congeDto)
        {
            await _congeRepository.AddedAsync(new Conge
            {
                CinEmploye = congeDto.CIN,
                DateDebut = congeDto.DateDebut,
                DateFin = congeDto.DateFin,
                Statut = CongeStatusEnum.Accepter.ToString()
            });

            await _unitOfWork.CommitAsync();
        }
    }
}
