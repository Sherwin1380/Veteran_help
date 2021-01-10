using Sasasaa.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Sasasaa.Controllers
{
    public class VeteranController : ApiController
    {
        // GET: Slot
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Post([FromBody] Veteran p)
        {
            if (p == null)
            {
                return NotFound();
            }
            var t = Database.AddVeteran(p);
            return Ok(t);
        }
    }

}