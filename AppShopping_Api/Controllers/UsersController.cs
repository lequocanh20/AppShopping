using AppShopping_Application.Systems.Users;
using AppShopping_ViewModels.Systems.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        /* Dùng FromBody thì lấy từ json đã serialize bên UserApiClient truyền vô được
        còn FromForm thì lấy từ form */
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Truyền request vào hàm Authencate của UserService bên Domain và trả về một JWT
            var result = await _userService.Authencate(request);

            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }


            return Ok(result);
        }

        [HttpPost]
        // Cho phép người lạ truy cập
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{token}")]
        [Authorize]
        public async Task<IActionResult> GetById(string token)
        {
            var identity = this.ValidateToken(token);

            // Get the claims values
            Guid userId = Guid.Parse(identity?.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                               .Select(c => c.Value).SingleOrDefault());
            var user = await _userService.GetById(userId);
            return Ok(user);
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));


            // Giải mã thông tin claim mà ta đã gắn cho token ấy ( định nghĩa claim trong UserService.cs )
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            // trả về một principal có token đã giải mã
            return principal;
        }
    }
}
