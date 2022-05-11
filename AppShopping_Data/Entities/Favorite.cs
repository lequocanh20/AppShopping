using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Data.Entities
{
    public class Favorite
    {
        public int ProductId { get; set; }

        public Guid UserId { get; set; }

        public AppUser AppUser { get; set; }

        public Product Product {get; set;}
    }
}
