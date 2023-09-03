using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Annonce;
using PortailRH.BLL.Dtos.Paiement;
using PortailRH.BLL.Services.PaiementService;

namespace PortailRH.Web.Controllers.Administrateur
{
    /// <summary>
    /// PaiementController
    /// </summary>
    public class PaiementController : Controller
    {
        /// <summary>
        /// IPaiementService
        /// </summary>
        private readonly IPaiementService _paiementService;

        /// <summary>
        /// ILogger<PaiementController>
        /// </summary>
        private readonly ILogger<PaiementController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="paiementService">IPaiementService</param>
        /// <param name="logger">ILogger<PaiementController></param>
        public PaiementController(IPaiementService paiementService, ILogger<PaiementController> logger)
        {
            _paiementService = paiementService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PaiementPaginatedListDto>> Index(PaiementSearchDto searchDto)
        {
            try
            {
                var paiements = await _paiementService.GetPayments(searchDto);

                return View(paiements);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de la récupération des données !";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> NouveauPaiement(int mois, int annee)
        {
            try
            {
                await _paiementService.AddNewPayment(mois, annee);

                TempData["SuccessMessage"] = "Le paiement a été ajouté avec succès.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de l'ajout d'un nouveau paiement.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
