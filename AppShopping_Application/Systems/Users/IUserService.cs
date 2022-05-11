using AppShopping_ViewModels.Catalog.Products;
using AppShopping_ViewModels.Common;
using AppShopping_ViewModels.Systems.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Application.Systems.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);

        Task<ApiResult<string>> Register(RegisterRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);

        Task<List<UserViewModel>> GetAll();

        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<UserViewModel>> GetByUserName(string userName);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);

        Task<ApiResult<bool>> ConfirmEmail(ConfirmEmailViewModel request);

        Task<List<ProductViewModel>> GetAllProductFavorite(Guid userId);

        Task<ApiResult<bool>> AddFavorite(Guid userId, int productId);

        Task<ApiResult<bool>> DeleteFavorite(Guid userId, int productId);
    }
}
