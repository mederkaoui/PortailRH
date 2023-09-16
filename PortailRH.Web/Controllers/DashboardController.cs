using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Dashboard;
using PortailRH.BLL.Services.DashboardService;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// DashboardController
    /// </summary>
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
