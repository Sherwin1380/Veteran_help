using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sasasaa.Models
{
    public class Accepted
    {

        public string name { get; set; }

        public string orderno { get; set; }


        public Accepted()
        { }

        public Accepted(Accepted i)
        {
            orderno = i.orderno;
            name = i.orderno;
        }
    }
}