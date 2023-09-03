using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace PortailRH.Web.Classes
{
    /// <summary>
    /// SessionExtensions
    /// </summary>
    public static class SessionExtensions
    {
        public static T GetEncryptedObject<T>(this ISession session, string key, IDataProtector dataProtector)
        {
            var encryptedData = session.GetString(key);
            if (encryptedData != null)
            {
                var decryptedData = dataProtector.Unprotect(encryptedData);
                return JsonConvert.DeserializeObject<T>(decryptedData);
            }
            return default(T);
        }
    }
}
