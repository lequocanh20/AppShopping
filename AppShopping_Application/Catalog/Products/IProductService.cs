using AppShopping_ViewModels.Catalog.Products;
using AppShopping_ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Application.Catalog.Products
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAll();

        Task<ProductViewModel> GetById(int productId);

        Task<List<ProductViewModel>> GetFeaturedProducts(int take);

        // trả về kiểu int là trả về mã SP ta vừa tạo
        // tham số không phải lúc nào cũng truyền vào 1 Product view model, nhiều khi sẽ bị thừa
        Task<int> Create(ProductCreateRequest request);

        // Create và Update truyền 1 Dtos vào phương thức
        // Dtos là Data transfer object ( giống view model truyền cho 1 view )
        Task<int> Update(ProductUpdateRequest request);

        // để xóa thì ta chỉ cần truyền vào 1 product id
        Task<int> Delete(int productId);

        Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        Task<PagedResult<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

        Task<bool> DecreaseStock(int productId, int quantity);

        Task<int> AddReview(ProductDetailViewModel model);
    }
}
