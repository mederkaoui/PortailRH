using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Presence;
using PortailRH.BLL.Services.PresenceService;

namespace PortailRH.Web.Controllers.Administrateur
{
    /// <summary>
    /// PresenceController
    /// </summary>
    public class PresenceController : Controller
    {
        /// <summary>
        /// IPresenceService
        /// </summary>
        private readonly IPresenceService _presenceService;

        /// <summary>
        /// ILogger<PresenceController>
        /// </summary>
        private readonly ILogger<PresenceController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="presenceService">IPresenceService</param>
        /// <param name="logger">ILogger<PresenceController></param>
        public PresenceController(IPresenceService presenceService, ILogger<PresenceController> logger)
        {
            _presenceService = presenceService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<PresenceDto>>> Index(PresenceSearchDto searchDto)
        {
            try
            {
                var presence = await _presenceService.GetPresencesByMonths(searchDto);

                return View(presence);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la récupération des données.";

                return View();
            }
        }
    }
}
