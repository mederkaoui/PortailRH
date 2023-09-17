using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.DataProtection;
using PortailRH.BLL.Dtos.Authentification;

namespace PortailRH.Web.Classes
{
    /// <summary>
    /// CustomAuthorizeFilter
    /// </summary>
    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly bool _isAdminRequired;

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
        /// <param name="isAdminRequired">isAdminRequired</param>
        /// <param name="dataProtectionProvider">IHttpContextAccessor</param>
        /// <param name="httpContextAccessor">IDataProtectionProvider</param>
        public CustomAuthorizeFilter(bool isAdminRequired, IDataProtectionProvider dataProtectionProvider, 
            IHttpContextAccessor httpContextAccessor)
        {
            _isAdminRequired = isAdminRequired;
            _dataProtector = dataProtectionProvider.CreateProtector("LoginDtoProtection");
            _httpContextAccessor = httpContextAccessor;
            _currentUser = new UserDto();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _currentUser = _httpContextAccessor.HttpContext!.Session.GetEncryptedObject<UserDto>("LoggedInUser", _dataProtector);

            // Check if the user is authenticated
            if (_currentUser.CIN == null || _currentUser.CIN.Length <= 0)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if the user has the 'isAdmin' property in the session
            var isAdmin = _currentUser.EstAdministarteur ?? false;

            // Check if 'isAdmin' matches the required authorization level
            if (_isAdminRequired && !isAdmin)
            {
                //context.Result = new ForbidResult();
                // Set the response status code to 401 (Unauthorized)
                context.HttpContext.Response.StatusCode = 401;

                // Specify the full path to the "NotAuthorized" view
                context.Result = new ViewResult
                {
                    ViewName = "~/Views/Home/NotAuthorized.cshtml"
                };
                return;
            }
        }
    }
}
