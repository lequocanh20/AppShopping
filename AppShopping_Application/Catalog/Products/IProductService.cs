using AppShopping_ViewModels.Catalog.Products;
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
    }
}
