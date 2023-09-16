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
        /// ILogger<DashboardService>
        /// </summary>
        private readonly ILogger<DashboardService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeRepository">IGenericRepository<Employe></param>
        /// <param name="logger">ILogger<DashboardService></param>
        public DashboardService(IGenericRepository<Employe> employeRepository, ILogger<DashboardService> logger)
        {
            _employeRepository = employeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get Main Char Data
        /// </summary>
        /// <param name="year">year</param>
        /// <returns>ICollection<EmployeChartDataDto></returns>
        public async Task<ICollection<EmployeChartDataDto>> GetChartData(int? year)
        {
            var data = await _employeRepository.GetListAsync(
                                                            //predicate: x => x.Contrats.First().DateDebut!.Value.Year == (year ?? DateTime.Now.Year),
                                                            include: inc => inc.Include(x => x.Contrats)
                                                        );

            var chartData = data
                .SelectMany(employe => employe.Contrats)
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
    }
}
