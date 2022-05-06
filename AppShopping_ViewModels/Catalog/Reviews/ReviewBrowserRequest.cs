using AppShopping_ViewModels.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_ViewModels.Catalog.Reviews
{
    public class ReviewBrowserRequest
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
