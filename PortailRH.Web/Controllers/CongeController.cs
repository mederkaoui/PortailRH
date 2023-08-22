using Microsoft.AspNetCore.Mvc;

namespace PortailRH.Web.Controllers
{
    public class CongeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
