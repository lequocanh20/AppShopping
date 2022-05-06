using AppShopping_ViewModels.Catalog.Products;
using AppShopping_ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_ApiIntegration
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetPagings(GetManageProductPagingRequest request);

        Task<PagedResult<ProductViewModel>> GetAllByCategoryPaging(GetPublicProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<bool> UpdateProduct(ProductUpdateRequest request);

        Task<bool> DeleteProduct(int id);

        Task<ProductViewModel> GetById(int id);

        Task<List<ProductViewModel>> GetFeaturedProducts(string languageId, int take);

        Task<string> AddReview(ProductDetailViewModel model);

        Task<List<ProductViewModel>> GetAll();
    }
}
