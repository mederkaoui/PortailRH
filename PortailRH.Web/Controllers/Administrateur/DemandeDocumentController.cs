using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PortailRH.BLL.Dtos.DemandeDocument;
using PortailRH.BLL.Services.DemandeDocumentService;

namespace PortailRH.Web.Controllers.Administrateur
{
    /// <summary>
    /// DemandeDocumentController
    /// </summary>
    public class DemandeDocumentController : Controller
    {
        /// <summary>
        /// IDemandeDocumentService
        /// </summary>
        private readonly IDemandeDocumentService _demandeDocumentService;

        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// ILogger<DemandeDocumentController>
        /// </summary>
        private readonly ILogger<DemandeDocumentController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="demandeDocumentService">IDemandeDocumentService</param>
        /// <param name="webHostEnvironment">IWebHostEnvironment</param>
        /// <param name="logger">ILogger<DemandeDocumentController></param>
        public DemandeDocumentController(IDemandeDocumentService demandeDocumentService,
            IWebHostEnvironment webHostEnvironment, ILogger<DemandeDocumentController> logger)
        {
            _demandeDocumentService = demandeDocumentService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<DemandeDocumentPaginatedListDto>> Index(DemandeDocumentSearchDto searchDto)
        {
            try
            {
                var demandes = await _demandeDocumentService.GetRequestedDocumnet(searchDto);

                return View(demandes);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la récupération des données.";

                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult<DemandeDocumentDto>> GetDetailsDemande(int id)
        {
            try
            {
                var demande = await _demandeDocumentService.GetDetailsDemande(id);
                ViewBag.TypesDocument = await _demandeDocumentService.GetTypesDocument();

                return PartialView("_DetailsDemande", demande);
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de la récupération des données.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AjouterDocument(UploadDocumentDto uploadDocumentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (uploadDocumentDto.Document != null && uploadDocumentDto.Document.Length > 0)
                    {
                        string uploadFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents/demandes");
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadDocumentDto.Document.FileName;
                        string filePath = Path.Combine(uploadFolderPath, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await uploadDocumentDto.Document.CopyToAsync(fileStream);
                        }

                        uploadDocumentDto.DocumentNom = uniqueFileName;
                    }

                    await _demandeDocumentService.AddedDocument(uploadDocumentDto);

                    TempData["SuccessMessage"] = "Le document a été envoyé avec succès.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Les informations que vous avez entrées ne sont pas correctes.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Une erreur s'est produite lors de l'envoi du document.\r\n\r\n\r\n\r\n\r\n";

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
