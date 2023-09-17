using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Authentification;
using PortailRH.BLL.Dtos.Employe;
using PortailRH.BLL.Services.EmployeService;
using PortailRH.Web.Classes;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// EmpSettingController
    /// </summary>
    [CustomAuthorize(isAdminRequired: false)]
    public class EmpSettingController : Controller
    {
        /// <summary>
        /// IEmployeService
        /// </summary>
        private readonly IEmployeService _employeService;

        /// <summary>
        /// IDataProtector
        /// </summary>
        private readonly IDataProtector _dataProtector;

        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// ILogger<EmpSettingController>
        /// </summary>
        private readonly ILogger<EmpSettingController> _logger;

        /// <summary>
        /// UserDto
        /// </summary>
        private UserDto _currentUser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeService">IEmployeService</param>
        /// <param name="logger">ILogger<EmpSettingController></param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="dataProtectionProvider">IDataProtectionProvider</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        public EmpSettingController(IEmployeService employeService, ILogger<EmpSettingController> logger,
            IWebHostEnvironment webHostEnvironment, IDataProtectionProvider dataProtectionProvider, IHttpContextAccessor httpContextAccessor)
        {
            _employeService = employeService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _dataProtector = dataProtectionProvider.CreateProtector("LoginDtoProtection");
            _httpContextAccessor = httpContextAccessor;
            _currentUser = new UserDto();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);
                var employe = await _employeService.GetDetailsEmploye(_currentUser.CIN!);
                ViewBag.ListsData = await _employeService.GetSelectListsData();

                return View(employe);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur est produite lors de la récupération des données.";

                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ModifierInformations(NouvelEmployeDto employe)
        {
            try
            {
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);

                if (ModelState.IsValid)
                {
                    // Handle image upload
                    if (employe.Photo != null && employe.Photo.Length > 0)
                    {
                        string uploadFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images/EmployeImages");
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + employe.Photo.FileName;
                        string filePath = Path.Combine(uploadFolderPath, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await employe.Photo.CopyToAsync(fileStream);
                        }

                        employe.PhotoName = uniqueFileName; // Update the Photo property with the image file name
                    }

                    var updatedEmploye = await _employeService.UpdateEmploye(_currentUser.CIN!, employe);

                    TempData["SuccessMessage"] = "Les informations ont été modifiées avec succès!";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la Modification des informations.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
