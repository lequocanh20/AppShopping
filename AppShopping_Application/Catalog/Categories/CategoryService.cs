using AppShopping_Application.Common;
using AppShopping_Data.EF;
using AppShopping_Data.Entities;
using AppShopping_Utilities.Exceptions;
using AppShopping_ViewModels.Catalog.Categories;
using AppShopping_ViewModels.Catalog.Products;
using AppShopping_ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly AppShoppingDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public CategoryService(AppShoppingDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(CategoryCreateRequest request)
        {
            var category = new Category()
            {
                Name = request.Name
            };

            if (request.ImageCate != null)
            {
                category.ImageCate = await this.SaveFile(request.ImageCate);
            }
            else
            {
                category.ImageCate = "/user-content/no-image.png";
            }


            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<int> Delete(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null) throw new AppShoppingException($"Không thể tìm danh mục có ID: {categoryId} ");

            _context.Categories.Remove(category);

            return await _context.SaveChangesAsync();
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

        public async Task<PagedResult<CategoryViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            var query = from c in _context.Categories
                        select new { c };

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.c.Name.Contains(request.Keyword));

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.c.Id,
                    Name = x.c.Name,
                    ImageCate = x.c.ImageCate
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<CategoryViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public async Task<CategoryViewModel> GetById(int id)
        {
            var query = from c in _context.Categories
                        where c.Id == id
                        select new { c };

            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                ImageCate = x.c.ImageCate
            }).FirstOrDefaultAsync();
        }

        public async Task<int> Update(CategoryUpdateRequest request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if (category == null) throw new AppShoppingException($"Không thể tìm danh mục có ID: {request.Id} ");

            category.Name = request.Name;
            if (request.ImageCate != null)
            {
                category.ImageCate = await this.SaveFile(request.ImageCate);
            }
            else
            {
                category.ImageCate = "/user-content/no-image.png";
            }

            return await _context.SaveChangesAsync();
        }
    }
}
