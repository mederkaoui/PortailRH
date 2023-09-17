using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortailRH.BLL.Dtos.Dashboard;
using PortailRH.DAL.Entities;
using PortailRH.DAL.Repositories.GenericRepository;

namespace PortailRH.BLL.Services.DashboardService
{
    /// <summary>
    /// DashboardService
    /// </summary>
    public class DashboardService : IDashboardService
    {
        /// <summary>
        /// IGenericRepository<Employe>
        /// </summary>
        private readonly IGenericRepository<Employe> _employeRepository;

        /// <summary>
        /// IGenericRepository<Contrat>
        /// </summary>
        private readonly IGenericRepository<Contrat> _contratRepository;

        /// <summary>
        /// IGenericRepository<Paiement>
        /// </summary>
        private readonly IGenericRepository<Paiement> _paiementRepository;

        /// <summary>
        /// IGenericRepository<Absence>
        /// </summary>
        private readonly IGenericRepository<Absence> _absenceRepository;

        /// <summary>
        /// ILogger<DashboardService>
        /// </summary>
        private readonly ILogger<DashboardService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeRepository">IGenericRepository<Employe></param>
        /// <param name="contratRepository">IGenericRepository<Contrat></param>
        /// <param name="paiementRepository">IGenericRepository<Paiement></param>
        /// <param name="absenceRepository">IGenericRepository<Absence></param>
        /// <param name="logger">ILogger<DashboardService></param>
        public DashboardService(IGenericRepository<Employe> employeRepository, IGenericRepository<Contrat> contratRepository,
            IGenericRepository<Paiement> paiementRepository, IGenericRepository<Absence> absenceRepository, ILogger<DashboardService> logger)
        {
            _employeRepository = employeRepository;
            _contratRepository = contratRepository;
            _paiementRepository = paiementRepository;
            _absenceRepository = absenceRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get Main Char Data
        /// </summary>
        /// <param name="year">year</param>
        /// <returns>ICollection<EmployeChartDataDto></returns>
        public async Task<ICollection<EmployeChartDataDto>> GetChartData(int? year)
        {
            year = year != null && year > 0 ? year : DateTime.Now.Year;
            var data = await _contratRepository.GetListAsync(
                                                            predicate: x => x.DateDebut!.Value.Year == year &&
                                                                            x.CinEmployeNavigation.EstSupprime == false
                                                        );

            var chartData = data
                .DistinctBy(x => x.CinEmploye)
                .GroupBy(contrat => new { Year = contrat.DateDebut!.Value.Year, Month = contrat.DateDebut.Value.Month })
                .Select(group => new
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    EmployeeCount = group.Count()
                })
                .ToList();

            _logger.LogInformation("Get Main Char Data");

            return chartData.Select(x => new EmployeChartDataDto
            {
                Year = x.Year,
                Month = x.Month,
                EmployeeCount = x.EmployeeCount
            }).ToList();
        }

        /// <summary>
        /// Get Statistics Data
        /// </summary>
        /// <returns>StatisticsDataDto</returns>
        public async Task<StatisticsDataDto> GetStatisticsData()
        {
            var employesCount = await _employeRepository.GetCount(
                                                                predicate: x => x.EstSupprime == false
                                                            );

            var absences = await _absenceRepository.GetListAsync(
                                                                predicate: x => x.DateDebut!.Value.Year == DateTime.Now.Year &&
                                                                                x.DateDebut.Value.Month == DateTime.Now.Month
                                                            );

            var paiments = await _paiementRepository.GetListAsync(
                                                                predicate: x => x.DatePaiement!.Value.Year == DateTime.Now.Year
                                                            );

            return new StatisticsDataDto
            {
                Absences = absences.Sum(x => (x.DateFin - x.DateDebut)!.Value.Days),
                Paiements = paiments.Sum(x => x.Salaire),
                TotalEmployes = employesCount
            };
        }
    }
}
