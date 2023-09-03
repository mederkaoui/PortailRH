using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Absence;
using PortailRH.BLL.Services.AbsenceService;
using PortailRH.BLL.Services.EmployeService;
using PortailRH.DAL.Entities;

namespace PortailRH.Web.Controllers.Administrateur
{
    /// <summary>
    /// AbsenceController
    /// </summary>
    public class AbsenceController : Controller
    {
        /// <summary>
        /// IAbsenceService
        /// </summary>
        private readonly IAbsenceService _absenceService;

        /// <summary>
        /// IEmployeService
        /// </summary>
        private readonly IEmployeService _employeService;

        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// ILogger<AbsenceController>
        /// </summary>
        private readonly ILogger<AbsenceController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="absenceService">IAbsenceService</param>
        /// <param name="employeService">IEmployeService</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="logger"><ILogger<AbsenceController>></param>
        public AbsenceController(IAbsenceService absenceService, IEmployeService employeService,
            IWebHostEnvironment webHostEnvironment, ILogger<AbsenceController> logger)
        {
            _absenceService = absenceService;
            _employeService = employeService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<AbsencePaginatedListDto>> Index(AbsenceSearchDto searchDto)
        {
            try
            {
                var absences = await _absenceService.GetAbsencesPaginatedList(searchDto);
                ViewBag.Types = await _absenceService.GetAbsenceTypes();
                ViewBag.Employes = await _employeService.GetEmployesForConge();

                return View(absences);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lorsque nous essayons de récupérer des données";

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
                absenceDto.Justifie = Request.Form["Justifie"] == "on";
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
    }
}
