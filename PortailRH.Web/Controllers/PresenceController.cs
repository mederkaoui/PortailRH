using Microsoft.AspNetCore.Mvc;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// PresenceController
    /// </summary>
    public class PresenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
