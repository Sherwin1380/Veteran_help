

namespace Sasasaa.Models
{
    public class Password
    {
        public string mail { get; set; }

        public string pass { get; set; }

        public int type { get; set; }

        public Password()
        {
        }

        public Password(Password p)
        {
            mail = p.mail;
            pass = p.pass;
            type = p.type;
        }
    }
}