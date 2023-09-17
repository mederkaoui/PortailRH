using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Authentification;
using PortailRH.BLL.Dtos.Conge;
using PortailRH.BLL.Services.CongeService;
using PortailRH.Web.Classes;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// EmpCongeController
    /// </summary>
    [CustomAuthorize(isAdminRequired: false)]
    public class EmpCongeController : Controller
    {
        /// <summary>
        /// ICongeService
        /// </summary>
        private readonly ICongeService _congeService;

        /// <summary>
        /// ILogger<EmpCongeController>
        /// </summary>
        private readonly ILogger<EmpCongeController> _logger;

        /// <summary>
        /// IDataProtector
        /// </summary>
        private readonly IDataProtector _dataProtector;

        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// UserDto
        /// </summary>
        private UserDto _currentUser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="congeService">ICongeService</param>
        /// <param name="logger"><EmpCongeController></param>
        /// <param name="dataProtectionProvider">IDataProtectionProvider</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        public EmpCongeController(ICongeService congeService, ILogger<EmpCongeController> logger,
            IDataProtectionProvider dataProtectionProvider, IHttpContextAccessor httpContextAccessor)
        {
            _congeService = congeService;
            _logger = logger;
            _dataProtector = dataProtectionProvider.CreateProtector("LoginDtoProtection");
            _httpContextAccessor = httpContextAccessor;
            _currentUser = new UserDto();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CongeDto>>> Index()
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);

                var conges = await _congeService.GetEmployeConges(_currentUser.CIN!);

                return View(conges);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la récupération des congés";

                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DemandeConge(AddCongeDto addCongeDto)
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);

                addCongeDto.CIN = _currentUser.CIN!;
                addCongeDto.IsAdministrateur = false;

                await _congeService.AddConge(addCongeDto);

                TempData["SuccessMessage"] = "La demande a été ajoutée avec succès !";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'ajout du demande.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> SupprimerConge(int id)
        {
            try
            {
                await _congeService.DeleteConge(id);

                TempData["SuccessMessage"] = "Congé est supprimer avec succès!";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la suppression de congé.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<ActionResult<CongeDto>> GetCongeEditInformations(int id)
        {
            try
            {
                var conge = await _congeService.GetCongeDetails(id);

                return PartialView("_EditConge", conge);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la récupération des détails de congé.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<ActionResult<CongeDto>> GetCongeDetails(int id)
        {
            try
            {
                var conge = await _congeService.GetCongeDetails(id);

                return PartialView("_DetailsConge", conge);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la récupération des détails de congé.";

                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> ModifierConge([FromForm] int id, EditCongeDto congeDto)
        {
            try
            {
                await _congeService.UpdateConge(id, congeDto);

                TempData["SuccessMessage"] = "Le congé a été modifié avec succès !";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la modification de congé.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
