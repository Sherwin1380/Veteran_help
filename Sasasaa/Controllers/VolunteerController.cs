
using Sasasaa.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Sasasaa.Controllers
{
    public class VolunteerController : ApiController
    {
        // GET: Slot
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Post([FromBody] Volunteer p)
        {
            if (p == null)
            {
                return NotFound();
            }
            var t = Database.AddVolunteer(p);
            return Ok(t);
        }
    }

}