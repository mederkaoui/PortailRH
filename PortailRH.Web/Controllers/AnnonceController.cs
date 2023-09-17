using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Annonce;
using PortailRH.BLL.Services.AnnonceService;
using PortailRH.Web.Classes;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// AnnonceController
    /// </summary>
    [CustomAuthorize(isAdminRequired: true)]
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

        [HttpPost]
        public async Task<IActionResult> AjouterAnnonce(AnnonceDto annonceDto)
        {
            try
            {
                await _annonceService.AddAnnonce(annonceDto);

                TempData["SuccessMessage"] = "L'annonce a été ajoutée avec succès.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de l'ajout de l'annonce.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
