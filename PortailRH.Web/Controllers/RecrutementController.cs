using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Recrutement;
using PortailRH.BLL.Services.DepartementService;
using PortailRH.BLL.Services.RecrutementService;
using PortailRH.DAL.Entities;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// RecrutementController
    /// </summary>
    public class RecrutementController : Controller
    {
        /// <summary>
        /// IRecrutementService
        /// </summary>
        private readonly IRecrutementService _recrutementService;

        /// <summary>
        /// IDepartementService
        /// </summary>
        private readonly IDepartementService _departementService;

        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// ILogger<RecrutementController>
        /// </summary>
        private readonly ILogger<RecrutementController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recrutementService">IRecrutementService</param>
        /// <param name="departementService">IDepartementService</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="logger">ILogger<RecrutementController></param>
        public RecrutementController(IRecrutementService recrutementService, IDepartementService departementService, IWebHostEnvironment webHostEnvironment, ILogger<RecrutementController> logger)
        {
            _recrutementService = recrutementService;
            _departementService = departementService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(RecrutementSearchDto searchDto)
        {
            try
            {
                var lstRecrutements = await _recrutementService.GetRecrutementPaginatedList(searchDto);
                ViewBag.ListDepartements = await _departementService.GetDepartemtnsWithFonction();

                return View(lstRecrutements);
            }
            catch
            {
                ViewBag.ListDepartements = await _departementService.GetDepartemtnsWithFonction();

                TempData["ErrorMessage"] = "L'obtention de la liste des recrutements a rencontré une erreur.";

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> SupprimerRecrutement(int id)
        {
            try
            {
                await _recrutementService.DeleteRecrutement(id);

                TempData["SuccessMessage"] = "Le recrutement a été supprimé avec succès.";

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la suppression du recrutement.";

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult<GetRecrutementDto>> GetDetailsRecrutement(int id)
        {
            try
            {
                var detailsRecrutement = await _recrutementService.GetDetialsRecrutement(id);

                return PartialView("_DetailsRecrutement", detailsRecrutement);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'obtention des détails du recrutement.";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AjouterRecrutement(NouveauxRecrutementDto nouveauxRecrutement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (nouveauxRecrutement.Document != null && nouveauxRecrutement.Document.Length > 0)
                    {
                        string uploadFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents/cv");
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + nouveauxRecrutement.Document.FileName;
                        string filePath = Path.Combine(uploadFolderPath, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await nouveauxRecrutement.Document.CopyToAsync(fileStream);
                        }

                        nouveauxRecrutement.NomDocument = uniqueFileName;
                    }

                    await _recrutementService.AddNewRecrutement(nouveauxRecrutement);

                    TempData["SuccessMessage"] = "Le recrutement a été ajouté avec succès.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Les informations que vous avez saisies ne sont pas correctes.";

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'ajout du recrutement.";

                return RedirectToAction("Index");
            }
        }
    }
}
