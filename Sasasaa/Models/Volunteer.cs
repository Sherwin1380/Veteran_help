using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sasasaa.Models
{

    public class Volunteer
    {
        public string name { get; set; }

        public int age { get; set; }

        public EndpointDetails location { get; set; }

        public string mail { get; set; }

        public string pass { get; set; }

        public Volunteer()
        {
            location = new EndpointDetails();
        }

        public Volunteer(Volunteer p)
        {
            name = p.name;
            age = p.age;
            location = p.location;
            mail = p.mail;
            pass = p.pass;
        }

    }
}