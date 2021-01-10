using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sasasaa.Models
{
    public class Items
    {
        public int[] itemlist { get; set; }

        public int[] quantity { get; set; }

        public string mail { get; set; }

        public Items()
        { }

        public Items(Items p)
        {
            itemlist = p.itemlist;
            quantity = p.quantity;
            mail = p.mail;
        }


    }
}