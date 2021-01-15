using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sasasaa.Models
{
    public class OrderListContent
    {
        public string items { get; set; }

        public double price { get; set; }

        public string veteran_mail { get; set; }

        public string volunteer_mail { get; set; }

        public int orderid { get; set; }

        public string Status { get; set; }


        public OrderListContent()
        { }
    }
}