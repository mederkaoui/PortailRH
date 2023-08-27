using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Presence;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;
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
        /// ILogger<PresenceService>
        /// </summary>
        private readonly ILogger<PresenceService> _logger;

        /// <summary>
        /// COnstructor
        /// </summary>
        /// <param name="presenceRepository">IGenericRepository<Presence></param>
        /// <param name="logger">ILogger<PresenceService></param>
        public PresenceService(IGenericRepository<Presence> presenceRepository, ILogger<PresenceService> logger)
        {
            _presenceRepository = presenceRepository;
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
                    TotalHours = group.Sum(p => (p.HeureSortie - p.HeureEntree)?.TotalHours)
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
    }
}
