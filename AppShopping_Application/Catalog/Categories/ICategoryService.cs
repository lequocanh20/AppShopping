﻿using AppShopping_ViewModels.Catalog.Categories;
using AppShopping_ViewModels.Catalog.Products;
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
        Task<int> Create(CategoryCreateRequest request);

        Task<int> Update(CategoryUpdateRequest request);

        Task<int> Delete(int categoryId);

        Task<PagedResult<CategoryViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        Task<CategoryViewModel> GetById(int id);

        Task<List<CategoryViewModel>> GetAll();
    }
}
