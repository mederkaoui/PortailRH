using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Presence;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
using PortailRH.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.PresenceService
{
    /// <summary>
    /// PresenceService.cs
    /// </summary>
    public class PresenceService : IPresenceService
    {
        /// <summary>
        /// IGenericRepository<Presence>
        /// </summary>
        private readonly IGenericRepository<Presence> _presenceRepository;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// ILogger<PresenceService>
        /// </summary>
        private readonly ILogger<PresenceService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="presenceRepository">IGenericRepository<Presence></param>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="logger">ILogger<PresenceService></param>
        public PresenceService(IGenericRepository<Presence> presenceRepository,
            IUnitOfWork unitOfWork, ILogger<PresenceService> logger)
        {
            _presenceRepository = presenceRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get Employes Absence Hours By Month
        /// </summary>
        /// <param name="searchDto">PresenceSearchDto</param>
        /// <returns>ICollection<PresenceDto></returns>
        public async Task<ICollection<PresenceDto>> GetPresencesByMonths(PresenceSearchDto searchDto)
        {
            var presences = await _presenceRepository.GetListAsync(
                                                                   predicate: x => (x.CinEmploye.Contains(searchDto.SearchQuery ?? string.Empty) || string.Concat(x.CinEmployeNavigation.Prenom, " ", x.CinEmployeNavigation.Nom).Contains(searchDto.SearchQuery ?? string.Empty)) &&
                                                                                x.DatePresence!.Value.Month == searchDto.Mois &&
                                                                                x.DatePresence.Value.Year == searchDto.Annee,
                                                                   include: inc => inc.Include(x => x.CinEmployeNavigation)
                                                                );

            //Total Work Days
            int totalWorkDays = 0;
            if (searchDto.Mois == DateTime.Now.Month && searchDto.Annee == DateTime.Now.Year )
            {
                DateTime currentDate = DateTime.Now;
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                TimeSpan daysPassed = currentDate - firstDayOfMonth;
                totalWorkDays = daysPassed.Days + 1;
            }
            else
            {
                totalWorkDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            }

            var groupedPresences = presences.GroupBy(
                p => p.CinEmploye,
                (key, group) => new
                {
                    CinEmploye = key,
                    NomEmploye = group.First().CinEmployeNavigation.Nom,
                    PrenomEmploye = group.First().CinEmployeNavigation.Prenom,
                    TotalHours = group.Sum(p => (p.HeureSortie - p.HeureEntree))
                }
            );

            return groupedPresences.Select(x => new PresenceDto
            {
                CIN = x.CinEmploye,
                Nom = x.NomEmploye,
                Prenom= x.PrenomEmploye,
                Mois = searchDto.Mois,
                // 9 => represent 8 of work and 1 for break
                HeuresAbsence = Convert.ToInt32((9 * totalWorkDays) - x.TotalHours)
            }).ToList();
        }

        /// <summary>
        /// Add employe presence
        /// </summary>
        /// <param name="presenceDto">AddPresenceDto</param>
        /// <returns></returns>
        public async Task AddEmployePresence(AddPresenceDto presenceDto)
        {
            await _presenceRepository.AddedAsync(new Presence
            {
                CinEmploye = presenceDto.CIN!,
                DatePresence = presenceDto.DatePresence,
                HeureEntree = 8,
                HeureSortie = 8 + presenceDto.HeuresTravaillees
            });

            await _unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Get Employe Presences
        /// </summary>
        /// <param name="cin">cin employe</param>
        /// <returns>ICollection<EmployePresenceDto></returns>
        public async Task<ICollection<EmployePresenceDto>> GetEmployePresences(string cin)
        {
            var presence = await _presenceRepository.GetListAsync(x => x.CinEmploye == cin);

            return presence.Select(x => new EmployePresenceDto
            {
                DatePresence = x.DatePresence,
                HeuresTravaillees = x.HeureSortie - x.HeureEntree
            }).ToList();
        }
    }
}
