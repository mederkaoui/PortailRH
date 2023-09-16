using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Annonce;
using PortailRH.BLL.Services.AnnonceService;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// EmpAnnonceController
    /// </summary>
    public class EmpAnnonceController : Controller
    {
        /// <summary>
        /// IAnnonceService
        /// </summary>
        private readonly IAnnonceService _annonceService;

        /// <summary>
        /// ILogger<EmpAbsenceController>
        /// </summary>
        private readonly ILogger<EmpAbsenceController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="annonceService"></param>
        /// <param name="logger"></param>
        public EmpAnnonceController(IAnnonceService annonceService, ILogger<EmpAbsenceController> logger)
        {
            _annonceService = annonceService;
            _logger = logger;
        }

        public async Task<ActionResult<AnnoncePaginatedListDto>> Index(AnnonceSearchDto searchDto)
        {
            try
            {
                var annonces = await _annonceService.GetAnnonce(searchDto);

                return View(annonces);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de la récupération des annonces.";

                return View();
            }
        }
    }
}
