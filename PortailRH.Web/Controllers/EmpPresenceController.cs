using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Authentification;
using PortailRH.BLL.Dtos.Presence;
using PortailRH.BLL.Services.PresenceService;
using PortailRH.Web.Classes;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// EmpPresenceController
    /// </summary>
    [CustomAuthorize(isAdminRequired: false)]
    public class EmpPresenceController : Controller
    {
        /// <summary>
        /// IPresenceService
        /// </summary>
        private readonly IPresenceService _presenceService;

        /// <summary>
        /// ILogger<EmpAbsenceController>
        /// </summary>
        private readonly ILogger<EmpAbsenceController> _logger;

        /// <summary>
        /// IDataProtector
        /// </summary>
        private readonly IDataProtector _dataProtector;

        /// <summary>
        /// UserDto
        /// </summary>
        private UserDto _currentUser;

        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="absenceService">IAbsenceService</param>
        /// <param name="dataProtectionProvider">IDataProtectionProvider</param>
        /// <param name="logger">ILogger<EmpAbsenceController></param>
        public EmpPresenceController(IPresenceService presenceService,
            IDataProtectionProvider dataProtectionProvider, IHttpContextAccessor httpContextAccessor, ILogger<EmpAbsenceController> logger)
        {
            _presenceService = presenceService;
            _dataProtector = dataProtectionProvider.CreateProtector("LoginDtoProtection");
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _currentUser = new UserDto();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<EmployePresenceDto>>> Index()
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);

                var result = await _presenceService.GetEmployePresences(_currentUser.CIN!);

                return View(result);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de la récupération des données.";
                
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AjouterPresence(AddPresenceDto presenceDto)
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);

                presenceDto.CIN = _currentUser.CIN;

                await _presenceService.AddEmployePresence(presenceDto);

                TempData["SuccessMessage"] = "La présence a été ajoutée avec succès.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de l'ajout de présence.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
