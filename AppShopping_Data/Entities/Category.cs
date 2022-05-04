using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageCate { get; set; }

        public List<Product> Products { get; set; }
    }
}
