using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_ViewModels.Systems.Users
{
    public class ConfirmEmailViewModel
    {
        public string token { get; set; }
        public string email { get; set; }
    }
}
