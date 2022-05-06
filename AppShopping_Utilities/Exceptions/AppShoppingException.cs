using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Utilities.Exceptions
{
    public class AppShoppingException : Exception
    {
        public AppShoppingException()
        {

        }
        public AppShoppingException(string message)
            : base(message)
        {
        }

        public AppShoppingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
