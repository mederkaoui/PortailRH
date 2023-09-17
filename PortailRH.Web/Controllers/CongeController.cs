using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.Conge;
using PortailRH.BLL.Dtos.Shared;
using PortailRH.BLL.Services.CongeService;
using PortailRH.BLL.Services.EmployeService;
using PortailRH.Web.Classes;

namespace PortailRH.Web.Controllers
{
    /// <summary>
    /// CongeController
    /// </summary>
    [CustomAuthorize(isAdminRequired: true)]
    public class CongeController : Controller
    {
        /// <summary>
        /// ICongeService
        /// </summary>
        private readonly ICongeService _congeService;

        /// <summary>
		/// IEmployeService
		/// </summary>
        private readonly IEmployeService _employeService;

        /// <summary>
        /// ILogger<CongeController>
        /// </summary>
        private readonly ILogger<CongeController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="congeService">ICongeService</param>
        /// <param name="employeService">IEmployeService</param>
        /// <param name="logger">ILogger<CongeController></param>
        public CongeController(ICongeService congeService, IEmployeService employeService, ILogger<CongeController> logger)
        {
            _congeService = congeService;
            _employeService = employeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<CongeDto>>> Index()
        {
            try
            {
                ViewBag.Conges = await _congeService.GetConges();
                ViewBag.Employes = await _employeService.GetEmployesForConge();

                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult<DemandesCongesPaginatedListDto>> DemandesConges(DemandesSearchDto searchDto)
        {
            try
            {
                var demandesConges = await _congeService.GetCongesRequests(searchDto);

                return View(demandesConges);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult<DemandesCongesPaginatedListDto>> TousLesConges(DemandesSearchDto searchDto)
        {
            try
            {
                var demandesConges = await _congeService.GetAllConges(searchDto);

                return View(demandesConges);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ModifierConge(UpdateCongeStatusDto updateCongeStatusDto)
        {
            try
            {
                await _congeService.UpdateCongeStatus(updateCongeStatusDto);

                TempData["SuccessMessage"] = updateCongeStatusDto.Status == CongeStatusEnum.Accepter ? "Congé est accepter avec succès!" : "Congé refuser avec succès!";

                return RedirectToAction(nameof(DemandesConges), new DemandesSearchDto());
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la modification de congé.";

                return RedirectToAction(nameof(DemandesConges), new DemandesSearchDto());
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteConge(int id)
        {
            try
            {
                await _congeService.DeleteConge(id);

                TempData["SuccessMessage"] = "Congé est supprimer avec succès!";

                return RedirectToAction(nameof(DemandesConges), new DemandesSearchDto());
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la suppression de congé.";

                return RedirectToAction(nameof(DemandesConges), new DemandesSearchDto());
            }
        }

        [HttpPost]
        public async Task<ActionResult> AjouterConge(AddCongeDto congeDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _congeService.AddConge(congeDto);

                    TempData["SuccessMessage"] = "Le congé a été ajouté avec succès !";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Les informations fournies ne sont pas valides.";

                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'ajout du congé.";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
