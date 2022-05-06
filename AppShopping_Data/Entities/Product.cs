using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal originPrice { get; set; }
        public int Stock { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { set; get; }

        // product image path
        public string ProductImage { get; set; }

        public Category Category { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
