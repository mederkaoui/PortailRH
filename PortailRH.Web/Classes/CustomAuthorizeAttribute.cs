using Microsoft.AspNetCore.Mvc;

namespace PortailRH.Web.Classes
{
    /// <summary>
    /// CustomAuthorizeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute(bool isAdminRequired = false) : base(typeof(CustomAuthorizeFilter))
        {
            Arguments = new object[] { isAdminRequired };
        }
    }
}
