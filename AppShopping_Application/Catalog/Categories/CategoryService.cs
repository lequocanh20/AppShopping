using AppShopping_Data.EF;
using AppShopping_ViewModels.Catalog.Catgories;
using AppShopping_ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly AppShoppingDbContext _context;

        public CategoryService(AppShoppingDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetAll()
        {
            var query = from c in _context.Categories
                        select new { c };

            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                ImageCate = x.c.ImageCate
            }).ToListAsync();
        }

        public async Task<ApiResult<CategoryViewModel>> GetById(int id)
        {
            var query = from c in _context.Categories
                        where c.Id == id
                        select new { c };

            return new ApiSuccessResult<CategoryViewModel>(await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                ImageCate = x.c.ImageCate
            }).FirstOrDefaultAsync());
        }
    }
}
