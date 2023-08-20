using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using PortailRH.BLL.Dtos.Employe;
using PortailRH.BLL.Services.EmployeService;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// EmployeController
    /// </summary>
    public class EmployeController : Controller
    {
        /// <summary>
        /// IEmployeService
        /// </summary>
        private readonly IEmployeService _employeService;

        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// ILogger<EmployeController>
        /// </summary>
        private readonly ILogger<EmployeController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeService">IEmployeService</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="logger">ILogger<EmployeController></param>
        public EmployeController(IEmployeService employeService, IWebHostEnvironment webHostEnvironment, ILogger<EmployeController> logger)
        {
            _employeService = employeService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

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

        public async Task<IActionResult> NouvelEmploye()
        {
            try
            {
                ViewBag.ListsData = await _employeService.GetSelectListsData();

                return View();
            }
            catch (Exception ex)
            {
                return View(); //TODO: handel pages 404, 500
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

                return RedirectToAction("Index");
            }
        }
    }
}
