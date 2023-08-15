using Microsoft.AspNetCore.Mvc;

namespace PortailRH.Web.Controllers
{
    public class EmployeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NouvelEmploye()
        {
            return View();
        }
    }
}
