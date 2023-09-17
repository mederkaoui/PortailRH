using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Absence;
using PortailRH.BLL.Dtos.Authentification;
using PortailRH.BLL.Services.AbsenceService;
using PortailRH.Web.Classes;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// EmpAbsenceController
    /// </summary>
    [CustomAuthorize(isAdminRequired: false)]
    public class EmpAbsenceController : Controller
    {
        /// <summary>
        /// IAbsenceService
        /// </summary>
        private readonly IAbsenceService _absenceService;

        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// ILogger<EmpAbsenceController>
        /// </summary>
        private readonly ILogger<EmpAbsenceController> _logger;

        /// <summary>
        /// IDataProtector
        /// </summary>
        private readonly IDataProtector _dataProtector;

        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// UserDto
        /// </summary>
        private UserDto _currentUser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="absenceService">IAbsenceService</param>
        /// <param name="dataProtectionProvider">IDataProtectionProvider</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="logger">ILogger<EmpAbsenceController></param>
        public EmpAbsenceController(IAbsenceService absenceService, IDataProtectionProvider dataProtectionProvider, 
            IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment, ILogger<EmpAbsenceController> logger)
        {
            _absenceService = absenceService;
            _dataProtector = dataProtectionProvider.CreateProtector("LoginDtoProtection");
            _httpContextAccessor = httpContextAccessor;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _currentUser = new UserDto();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<EmployeAbsenceDto>>> Index()
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);

                var result = await _absenceService.GetEmployeAbsences(_currentUser.CIN!);
                ViewBag.TypesAbsence = await _absenceService.GetAbsenceTypes();

                return View(result);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de la récupération des données.";

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> SupprimerAbsence(int id)
        {
            try
            {
                await _absenceService.DeleteAbsence(id);

                TempData["SuccessMessage"] = "L'absence a été supprimée avec succès.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lorsque nous avons essayé de supprimer l'absence.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailsAbsence(int id)
        {
            try
            {
                var detailsAbsence = await _absenceService.GetDetailsAbsence(id);

                return PartialView("_DetailsAbsence", detailsAbsence);
            }
            catch
            {
                var errorMessage = "Une erreur s'est produite lorsque nous avons essayé d'obtenir les détails d'absence";

                return Json(new { Error = errorMessage });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AjouterAbsence(AddAbsenceDto absenceDto)
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);
                absenceDto.Justifie = Request.Form["Justifie"] == "on";
                absenceDto.CIN = _currentUser.CIN!;

                if (ModelState.IsValid)
                {
                    if (absenceDto.Document != null && absenceDto.Document.Length > 0)
                    {
                        string uploadFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents/absence");
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + absenceDto.Document.FileName;
                        string filePath = Path.Combine(uploadFolderPath, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await absenceDto.Document.CopyToAsync(fileStream);
                        }

                        absenceDto.DocumentNom = uniqueFileName;
                    }


                    await _absenceService.AddAbsence(absenceDto);

                    TempData["SuccessMessage"] = "L'absence a été ajoutée avec succès.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Les informations que vous avez entrées ne sont pas correctes.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'ajout de l'absence.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetModidficationData(int id)
        {
            try
            {
                ViewBag.TypesAbsence = await _absenceService.GetAbsenceTypes();
                var editAbsence = await _absenceService.GetDetailsAbsence(id);

                return PartialView("_EditAbsence", editAbsence);
            }
            catch
            {
                var errorMessage = "Une erreur s'est produite lorsque nous avons essayé d'obtenir les détails d'absence";

                return Json(new { Error = errorMessage });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ModifierAbsence([FromForm] int id, EditAbsenceDto editAbsenceDto)
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);
                editAbsenceDto.CIN = _currentUser.CIN;
                editAbsenceDto.Justifie = Request.Form["Justifie"] == "on";

                if (ModelState.IsValid)
                {
                    if (editAbsenceDto.Document != null && editAbsenceDto.Document.Length > 0)
                    {
                        string uploadFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents/absence");
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + editAbsenceDto.Document.FileName;
                        string filePath = Path.Combine(uploadFolderPath, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await editAbsenceDto.Document.CopyToAsync(fileStream);
                        }

                        editAbsenceDto.DocumentNom = uniqueFileName;
                    }


                    await _absenceService.UpdateAbsence(id, editAbsenceDto);

                    TempData["SuccessMessage"] = "L'absence a été modifiée avec succès.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Les informations que vous avez entrées ne sont pas correctes.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lorsque nous avons essayé de modifier les informations de l'absence";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
