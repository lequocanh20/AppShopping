using AppShopping_Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public Guid UserId { set; get; }
        public int ProductId { get; set; }
        public double Rating { get; set; }
        public string Comments { get; set; }
        public DateTime PublishedDate { get; set; }
        public Status Status { get; set; }
        public Product Product { get; set; }
        public AppUser AppUser { get; set; }
    }
}
