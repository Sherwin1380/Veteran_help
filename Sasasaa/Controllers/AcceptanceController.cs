
using Sasasaa.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Sasasaa.Controllers
{
    public class AcceptanceController : ApiController
    {
        // GET: Slot
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Post([FromBody] Accepted p)
        {
            if (p == null)
            {
                return NotFound();
            }
            Database.saveorder(p);
            return Ok(1);
        }
    }

}