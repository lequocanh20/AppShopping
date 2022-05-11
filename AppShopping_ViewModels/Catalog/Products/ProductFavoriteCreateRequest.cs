using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_ViewModels.Catalog.Products
{
    public class ProductFavoriteCreateRequest
    {
        public string Token { get; set; }
        public int ProductId { get; set; }
    }
}
