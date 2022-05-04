using AppShopping_Data.EF;
using AppShopping_Data.Entities;
using AppShopping_ViewModels.Common;
using AppShopping_ViewModels.Systems.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Application.Systems.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppShoppingDbContext _context;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            AppShoppingDbContext context,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _config = config;
        }

        // Phương thức Login
        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            // Tìm xem tên user có tồn tại hay không
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null) return new ApiErrorResult<string>(new string("Tài khoản không tồn tại"));

            // Trả về một SignInResult, tham số cuối là IsPersistent kiểu bool
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);
            if (user.EmailConfirmed == false)
            {
                return new ApiErrorResult<string>(new string("Tài khoản chưa xác thực. Vui lòng xác thực tài khoản trước khi đăng nhập."));
            }
            else if (result.Succeeded && user.LockoutEnabled == true)
            {
                return new ApiErrorResult<string>(new string("Tài khoản của bạn đã bị khóa"));
            }
            else if (!result.Succeeded)
            {
                return new ApiErrorResult<string>(new string("Mật khẩu không đúng"));
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Role, "customer"),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.StreetAddress, user?.Address),
                new Claim(ClaimTypes.MobilePhone, user?.PhoneNumber),
                };
                // Lưu ý khi claim mà các thông tin bị null sẽ báo lỗi

                // Sau khi có được claim thì ta cần mã hóa nó
                // Tokens key và issuer nằm ở appsettings.json và truy cập được thông qua DI 1 Iconfig
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // 1 SecurityToken ( cần cài jwt )
                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMonths(1),
                    signingCredentials: creds);

                return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.StreetAddress, user?.Address),
                new Claim(ClaimTypes.MobilePhone, user?.PhoneNumber),
                };

                // Lưu ý khi claim mà các thông tin bị null sẽ báo lỗi

                // Sau khi có được claim thì ta cần mã hóa nó
                // Tokens key và issuer nằm ở appsettings.json và truy cập được thông qua DI 1 Iconfig
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // 1 SecurityToken ( cần cài jwt )
                var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                    _config["Tokens:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMonths(1),
                    signingCredentials: creds);

                return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
            }
        }
        public async Task<ApiResult<string>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            // Kiểm tra tài khoản đã tồn tại chưa
            if (user != null)
            {
                return new ApiErrorResult<string>(new string("Tên tài khoản đã tồn tại"));
            }

            // Kiểm tra email đã tồn tại chưa
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<string>(new string("Email đã tồn tại"));
            }

            var usersList = await _userManager.Users.ToListAsync();
            var userPhoneNumber = usersList.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber);

            if (userPhoneNumber != null)
            {
                return new ApiErrorResult<string>(new string("Số điện thoại đã tồn tại"));
            }

            if (request.Password != request.ConfirmPassword)
            {
                return new ApiErrorResult<string>(new string("Mật khẩu xác nhận không khớp với mật khẩu"));
            }

            user = new AppUser()
            {
                Email = request.Email,
                Address = request.Address,
                Name = request.Name,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                user.LockoutEnabled = false;
                await _context.SaveChangesAsync();
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                return new ApiSuccessResult<string>(token);
            }

            return new ApiErrorResult<string>(new string("Đăng ký không thành công"));
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<UserViewModel>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);

            var userVm = new UserViewModel()
            {
                UserName = user.UserName,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Id = user.Id,
            };

            foreach (var role in roles)
            {
                userVm.Roles = role.ToString();
            }

            return new ApiSuccessResult<UserViewModel>(userVm);
        }
    }
}
