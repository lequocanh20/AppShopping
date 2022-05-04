using AppShopping_ViewModels.Catalog.Catgories;
using AppShopping_ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Application.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<ApiResult<CategoryViewModel>> GetById(int id);

        Task<List<CategoryViewModel>> GetAll();
    }
}
