using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace AppShopping_AdminApp
{
    public class MyCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            context.Request.HttpContext.Items.Add("ExpiresUTC", context.Properties.ExpiresUtc);
        }
    }
}
