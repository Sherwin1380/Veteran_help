using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sasasaa.Models
{
    public class Order
    {
        public string name{ get; set; }

        public double lat{ get; set; }

        public double longitude{ get; set; }

         public int orderno { get; set; }

        public string mail { get; set; }

        public double price { get; set; }

        public string items { get; set; }




    }
}