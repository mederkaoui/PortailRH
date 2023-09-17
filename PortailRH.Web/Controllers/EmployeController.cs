using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Authentification;
using PortailRH.BLL.Dtos.Employe;
using PortailRH.BLL.Services.EmployeService;
using PortailRH.BLL.Services.TypeContratService;
using PortailRH.Web.Classes;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// EmployeController
    /// </summary>
    [CustomAuthorize(isAdminRequired: true)]
    public class EmployeController : Controller
    {
        /// <summary>
        /// IEmployeService
        /// </summary>
        private readonly IEmployeService _employeService;

        /// <summary>
        /// ITypeContratService
        /// </summary>
        private readonly ITypeContratService _typeContratService;

        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// ILogger<EmployeController>
        /// </summary>
        private readonly ILogger<EmployeController> _logger;

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
        /// <param name="employeService">IEmployeService</param>
        /// <param name="typeContratService">ITypeContratService</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="logger">ILogger<EmployeController></param>
        /// <param name="dataProtectionProvider">IDataProtectionProvider</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor</param>
        public EmployeController(IEmployeService employeService, ITypeContratService typeContratService, 
            IWebHostEnvironment webHostEnvironment, ILogger<EmployeController> logger, IDataProtectionProvider dataProtectionProvider, IHttpContextAccessor httpContextAccessor)
        {
            _employeService = employeService;
            _typeContratService = typeContratService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _dataProtector = dataProtectionProvider.CreateProtector("LoginDtoProtection");
            _httpContextAccessor = httpContextAccessor;
            _currentUser = new UserDto();
        }

        [HttpGet]
        public async Task<ActionResult<EmployePaginatedListDto>> Index()
        {
            try
            {
                var employes = await _employeService.GetEmployesPaginatedList(new EmployeSearchDto());

                return View(employes);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> NouvelEmploye()
        {
            try
            {
                ViewBag.ListsData = await _employeService.GetSelectListsData();

                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AjouterEmploye(NouvelEmployeDto employe)
        {
            try
            {
                ViewBag.ListsData = await _employeService.GetSelectListsData();

                if (ModelState.IsValid)
                {
                    // Handle image upload
                    if (employe.Photo != null && employe.Photo.Length > 0)
                    {
                        // Check if the uploaded file has an allowed image file extension
                        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; // Add more extensions if needed
                        string fileExtension = Path.GetExtension(employe.Photo.FileName).ToLowerInvariant();

                        if (allowedExtensions.Contains(fileExtension))
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
                        else
                        {
                            // Handle the case where the uploaded file is not an allowed image type
                            ModelState.AddModelError("Photo", "Please upload a valid image file.");
                            TempData["ErrorMessage"] = "Le fichier que vous avez téléchargé n'est pas une image valide. Veuillez télécharger une image valide.";
                            return View("NouvelEmploye", employe);
                        }
                    }

                    await _employeService.AddedEmploye(employe);

                    TempData["SuccessMessage"] = "Employé ajouté avec succès !";

                    return View("NouvelEmploye");
                }
                else
                {
                    // Validation failed, return the view with errors
                    return View("NouvelEmploye", employe);
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'ajout de l'employé.";

                return View("NouvelEmploye", employe);
            }
        }

        [HttpGet]
        public async Task<ActionResult<EmployePaginatedListDto>> GetFilteredEmployes(EmployeSearchDto employeSearchDto)
        {
            try
            {
                var employes = await _employeService.GetEmployesPaginatedList(employeSearchDto);

                return View("Index", employes);
            }
            catch
            {
                return View("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteEmploye(string cin)
        {
            try
            {
                await _employeService.DeleteEmploye(cin);

                TempData["SuccessMessage"] = "Employé est supprimer avec succès !";

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la suppression de l'employé.";

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetDetailsEmploye(string cin)
        {
            try
            {
                var detailsEmploye = await _employeService.GetDetailsEmploye(cin);

                return PartialView("_DetailsEmploye", detailsEmploye);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'obtenir les details de l'employé.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<ActionResult> ModifierEmploye(string cin)
        {
            try
            {
                ViewBag.ListsData = await _employeService.GetSelectListsData();
                var detailsEmploye = await _employeService.GetDetailsEmploye(cin);

                return View(detailsEmploye);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'obtenir les details de l'employé.";

                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ModifierEmploye(NouvelEmployeDto employe)
        {
            try
            {
                ViewBag.ListsData = await _employeService.GetSelectListsData();
                _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);

                if (ModelState.IsValid)
                {
                    // Handle image upload
                    if (employe.Photo != null && employe.Photo.Length > 0)
                    {
                        // Check if the uploaded file has an allowed image file extension
                        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; // Add more extensions if needed
                        string fileExtension = Path.GetExtension(employe.Photo.FileName).ToLowerInvariant();

                        if (allowedExtensions.Contains(fileExtension))
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
                        else
                        {
                            // Handle the case where the uploaded file is not an allowed image type
                            ModelState.AddModelError("Photo", "Please upload a valid image file.");
                            TempData["ErrorMessage"] = "Le fichier que vous avez téléchargé n'est pas une image valide. Veuillez télécharger une image valide.";
                            return View("NouvelEmploye", employe);
                        }
                    }

                    var updatedEmploye = await _employeService.UpdateEmploye(_currentUser.CIN!, employe);

                    TempData["SuccessMessage"] = "Employé modifié avec succès!";

                    return View("ModifierEmploye", updatedEmploye);
                }
                else
                {
                    return View("ModifierEmploye", employe);
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la Modification de l'employé.";

                return View("ModifierEmploye", employe);
            }
        }

        [HttpGet]
        public async Task<ActionResult<EmployePaginatedListDto>> AnciensEmployes(EmployeSearchDto searchDto)
        {
            try
            {
                var anciensEmployes = await _employeService.GetAnciensEmployesPaginatedList(searchDto ?? new EmployeSearchDto());
                ViewBag.TypesContat = await _typeContratService.GetTypesContrat();

                return View(anciensEmployes);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteEmployeDefinitely(string cin)
        {
            try
            {
                await _employeService.DeleteEmployeDefinitely(cin);

                TempData["SuccessMessage"] = "Employé est supprimer avec succès !";

                return RedirectToAction("AnciensEmployes");
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la suppression de l'employé.";

                return RedirectToAction("AnciensEmployes");
            }
        }

        [HttpPost]
        public async Task<ActionResult> NouveauContrat(EmployeNouveauContratDto nouveauContratDto)
        {
            try
            {
                await _employeService.NewContract(nouveauContratDto);

                TempData["SuccessMessage"] = "Nouveau contrat est sauvegardé avec succès!";

                return RedirectToAction("AnciensEmployes");
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la sauvegarde nouveau contrat!";

                return RedirectToAction("AnciensEmployes");
            }
        }
    }
}
