using AppShopping_ViewModels.Catalog.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public decimal OriginPrice { set; get; }
        public decimal Price { set; get; }
        public int CategoryId { set; get; }
        public int Stock { set; get; }
        public DateTime DateCreated { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string ProductImage { get; set; }
        public double Rating { get; set; }
        //public CategoryViewModel Category { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}
