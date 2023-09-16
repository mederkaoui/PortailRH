using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Authentification;
using PortailRH.BLL.Dtos.DemandeDocument;
using PortailRH.BLL.Services.DemandeDocumentService;
using PortailRH.Web.Classes;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// EmpDemandeDocumentController
    /// </summary>
    public class EmpDemandeDocumentController : Controller
    {
        /// <summary>
        /// IDemandeDocumentService
        /// </summary>
        private readonly IDemandeDocumentService _demandeDocumentService;

        /// <summary>
        /// IDataProtector
        /// </summary>
        private readonly IDataProtector _dataProtector;

        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// ILogger<EmpDemandeDocumentController>
        /// </summary>
        private readonly ILogger<EmpDemandeDocumentController> _logger;

        /// <summary>
        /// UserDto
        /// </summary>
        private UserDto _currentUser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="demandeDocumentService">IDemandeDocumentService</param>
        /// <param name="dataProtectionProvider">IDataProtectionProvider</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        /// <param name="logger">ILogger<EmpDemandeDocumentController></param>
        public EmpDemandeDocumentController(IDemandeDocumentService demandeDocumentService, IDataProtectionProvider dataProtectionProvider,
            IHttpContextAccessor httpContextAccessor, ILogger<EmpDemandeDocumentController> logger)
        {
            _demandeDocumentService = demandeDocumentService;
            _dataProtector = dataProtectionProvider.CreateProtector("LoginDtoProtection"); ;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _currentUser = new();
        }

        [HttpGet]
        public async Task<ActionResult<EmployeDemandeDocumentPaginatedListDto>> Index(EmployeDemandeDocumentSearchDto searchDto)
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);
                searchDto.CIN = _currentUser.CIN!;

                var demandes = await _demandeDocumentService.GetEmployePaginatedDemandes(searchDto);
                ViewBag.TypesDocument = await _demandeDocumentService.GetTypesDocument();

                return View(demandes);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de la récupération des données.";

                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AjouterDemande(AddDemandeDocumentDto addDemandeDocumentDto)
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);
                addDemandeDocumentDto.CIN = _currentUser.CIN!;

                await _demandeDocumentService.AddDemandeDocument(addDemandeDocumentDto);

                TempData["SuccessMessage"] = "La demande de document a été ajoutée avec succès.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'ajout de la demande.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> SupprimerDemande(int id)
        {
            try
            {
                await _demandeDocumentService.DeleteDemande(id);

                TempData["SuccessMessage"] = "La demande de document a été supprimée avec succès.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la suppression de la demande.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<ActionResult<DemandeDocumentDto>> GetDemandeDetails(int id)
        {
            try
            {
                var details = await _demandeDocumentService.GetDetailsDemande(id);

                return PartialView("_DetailsDemandeDocument", details);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la récupération des détails de la demande.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<ActionResult<DemandeDocumentDto>> GetDemandeModificationData(int id)
        {
            try
            {
                var details = await _demandeDocumentService.GetDetailsDemande(id);
                ViewBag.TypesDocument = await _demandeDocumentService.GetTypesDocument();

                return PartialView("_EditDemandeDocument", details);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la récupération des détails de la demande.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ModifierDemande([FromForm]int id, AddDemandeDocumentDto documentDto)
        {
            try
            {
                await _demandeDocumentService.UpdateDemandeDocument(id, documentDto);

                TempData["SuccessMessage"] = "La demande a été modifiée avec succès.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la modification de la demande.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
