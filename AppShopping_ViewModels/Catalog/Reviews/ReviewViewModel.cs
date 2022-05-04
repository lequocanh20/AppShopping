using AppShopping_ViewModels.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_ViewModels.Catalog.Reviews
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public Guid UserId { set; get; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public double Rating { get; set; }
        public string Comments { get; set; }
        public DateTime PublishedDate { get; set; }
        public Status Status { get; set; }
    }
}
