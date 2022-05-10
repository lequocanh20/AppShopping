using AppShopping_Application.Systems.Users;
using AppShopping_ViewModels.Systems.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
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
        private IHostingEnvironment _env;

        public UsersController(IUserService userService, IConfiguration configuration, IHostingEnvironment env)
        {
            _userService = userService;
            _configuration = configuration;
            _env = env;
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
            var user = await _userService.GetByUserName(request.UserName);
            var confirmationLink = Url.ActionLink("ConfirmedEmail", "ConfirmEmail", new { token = result.ResultObj, email = user.ResultObj.Email }, "https", Request.Host.Host + ":" + "5002");

            var email = new EmailService.EmailService();

            var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "TemplateEmail"
                            + Path.DirectorySeparatorChar.ToString()
                            + "Register_Confirm.html";

            var builder = new BodyBuilder();
            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            builder.HtmlBody = builder.HtmlBody.Replace("[name]", user.ResultObj.Name).Replace("[verification link]", confirmationLink);
            email.Send("lequocanh.qa@gmail.com", user.ResultObj.Email, "XÁC NHẬN TÀI KHOẢN", string.Format(builder.HtmlBody));
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

        [HttpGet("getByUserName/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _userService.GetByUserName(userName);
            if (!user.IsSuccessed)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpGet("getAllUser")]
        public async Task<IActionResult> GetAll()
        {
            var allUser = await _userService.GetAll();
            return Ok(allUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }


        //PUT: http://localhost/api/users/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}/roles")]
        public async Task<IActionResult> RoleAssign(Guid id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.RoleAssign(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // Đường dẫn ví dụ của GetAllPaging
        // http://localhost/api/users/paging?pageIndex=1&pageSize=10&Keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var products = await _userService.GetUsersPaging(request);
            return Ok(products);
        }

        [HttpPost("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailViewModel request)
        {
            var result = await _userService.ConfirmEmail(request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
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
