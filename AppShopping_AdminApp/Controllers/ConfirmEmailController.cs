using AppShopping_ApiIntegration;
using AppShopping_ViewModels.Systems.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AppShopping_AdminApp.Controllers
{
    [AllowAnonymous]
    public class ConfirmEmailController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConfirmEmailController(IUserApiClient userApiClient,
            IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmedEmail(string token, string email)
        {
            var confirmEmailVm = new ConfirmEmailViewModel()
            {
                token = token,
                email = email
            };

            var result = await _userApiClient.ConfirmEmail(confirmEmailVm);

            return View(result.IsSuccessed ? nameof(ConfirmedEmail) : "Error");
        }
    }
}
