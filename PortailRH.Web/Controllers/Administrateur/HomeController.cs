using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PortailRH.BLL.Dtos.Authentification;
using PortailRH.BLL.Services.AuthentificationService;

namespace PortailRH.Web.Controllers.Administrateur
{
    /// <summary>
    /// HomeController
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// IAuthentificationService
        /// </summary>
        private readonly IAuthentificationService _authentificationService;

        /// <summary>
        /// IDataProtector
        /// </summary>
        private readonly IDataProtector _dataProtector;

        /// <summary>
        /// ILogger<HomeController>
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authentificationService">IAuthentificationService</param>
        /// <param name="dataProtectionProvider">IDataProtectionProvider</param>
        /// <param name="logger">ILogger<HomeController></param>
        public HomeController(IAuthentificationService authentificationService,
            IDataProtectionProvider dataProtectionProvider, ILogger<HomeController> logger)
        {
            _authentificationService = authentificationService;
            _dataProtector = dataProtectionProvider.CreateProtector("LoginDtoProtection"); ;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return await AutoLogin();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (loginDto == null)
                {
                    TempData["ErrorMessage"] = "Données de connexion invalides.";

                    return RedirectToAction(nameof(Index));
                }

                loginDto.RememberMe = Request.Form["RememberMe"] == "on";

                var user = await _authentificationService.Login(loginDto);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "Le nom d'utilisateur ou le mot de passe est incorrect.";

                    return RedirectToAction("Index");
                }

                //Save user data into session
                var encryptedSessionData = _dataProtector.Protect(JsonConvert.SerializeObject(user));
                HttpContext.Session.SetString("LoggedInUser", encryptedSessionData);

                // Save user data in cookies for "Remember Me" users
                if (loginDto.RememberMe == true)
                {
                    var loginData = JsonConvert.SerializeObject(loginDto);
                    var encryptedData = _dataProtector.Protect(loginData);

                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddMonths(1),
                        HttpOnly = true
                    };
                    Response.Cookies.Append("LoggedInUserCookie", encryptedData, cookieOptions);
                }

                if (user.EstAdministarteur == true)
                {
                    return RedirectToAction("Index", "Employe");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ErrorMessage"] = "Le nom d'utilisateur ou le mot de passe est incorrect.";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Deconnecter()
        {
            try
            {
                // Remove session data
                HttpContext.Session.Remove("LoggedInUser");

                // Remove the "RememberMeCookie"
                Response.Cookies.Delete("LoggedInUserCookie", new CookieOptions
                {
                    Path = "/",
                    Expires = DateTime.UnixEpoch
                });
                return View(nameof(Index));
            }
            catch
            {
                return View(nameof(Index));
            }
        }

        /// <summary>
        /// Use Cookie Information to Get User Login
        /// </summary>
        /// <returns></returns>
        private async Task<IActionResult> AutoLogin()
        {
            if (Request.Cookies.TryGetValue("LoggedInUserCookie", out string userData))
            {
                try
                {
                    // Decrypt the cookie data
                    var decryptedData = _dataProtector.Unprotect(userData);

                    // Deserialize the user data
                    var loginDto = JsonConvert.DeserializeObject<LoginDto>(decryptedData);

                    // Authenticate the user
                    var user = await _authentificationService.Login(loginDto!);

                    if (user != null)
                    {
                        // Authentication successful, create a new session for the user
                        var encryptedData = _dataProtector.Protect(JsonConvert.SerializeObject(user));
                        HttpContext.Session.SetString("LoggedInUser", encryptedData);

                        // Redirect the user to a secure area or dashboard
                        if (user.EstAdministarteur == true)
                        {
                            return RedirectToAction("Index", "Employe");
                        }

                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    return View(nameof(Index));
                }
            }

            // If no valid cookie data or authentication fails, redirect to the login page.
            return View(nameof(Index));
        }
    }
}