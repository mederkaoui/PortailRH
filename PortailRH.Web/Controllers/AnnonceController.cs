using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Annonce;
using PortailRH.BLL.Services.AnnonceService;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// AnnonceController
    /// </summary>
    public class AnnonceController : Controller
    {
        /// <summary>
        /// IAnnonceService
        /// </summary>
        private readonly IAnnonceService _annonceService;

        /// <summary>
        /// ILogger<AnnonceController>
        /// </summary>
        private readonly ILogger<AnnonceController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="annonceService">IAnnonceService</param>
        /// <param name="logger">ILogger<AnnonceController></param>
        public AnnonceController(IAnnonceService annonceService, ILogger<AnnonceController> logger)
        {
            _annonceService = annonceService;
            _logger = logger;
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> SupprimerAnnonce(int id)
        {
            try
            {
                await _annonceService.DeleteAnnonce(id);

                TempData["SuccessMessage"] = "Annonce a été supprimée avec succès.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de la suppression de l'annonce.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
