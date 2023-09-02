using Microsoft.AspNetCore.Mvc;
using PortailRH.Web.Models;
using System.Diagnostics;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// ILogger<HomeController>
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            try
            {
                return RedirectToAction("Index", "Employe");
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}