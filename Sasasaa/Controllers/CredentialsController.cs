using Sasasaa.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Sasasaa.Controllers
{
    public class CredentialsController : ApiController
    {
        // GET: Slot
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Post([FromBody] Password p)
        {
            bool b = true;
            if (p == null)
            {
                return NotFound();
            }
            if (p.type == 1)
                b = Database.CheckpasswordVeteran(p);
            else
                b = Database.CheckpasswordVolunteer(p);
            return Ok(b);
        }
    }

}