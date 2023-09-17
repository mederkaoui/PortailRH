using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Dashboard;
using PortailRH.BLL.Services.DashboardService;
using PortailRH.Web.Classes;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// DashboardController
    /// </summary>
    [CustomAuthorize(isAdminRequired: true)]
    public class DashboardController : Controller
    {
        /// <summary>
        /// IDashboardService
        /// </summary>
        private readonly IDashboardService _dashboardService;

        /// <summary>
        /// ILogger<DashboardController>
        /// </summary>
        private readonly ILogger<DashboardController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dashboardService">IDashboardService</param>
        /// <param name="logger">ILogger<DashboardController></param>
        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
        {
            _dashboardService = dashboardService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<EmployeChartDataDto>>> Index(int year)
        {
            try
            {
                var data = await _dashboardService.GetChartData(year);
                ViewBag.StatisticData = await _dashboardService.GetStatisticsData();

                return View(data);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de la récupération des données.";

                return View();
            }
        }
    }
}
