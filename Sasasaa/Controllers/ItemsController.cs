using Sasasaa.Models;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Sasasaa.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ItemsController : ApiController
    {
        // GET: Slot
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Post([FromBody] Items p)
        {
            if (p == null)
            {
                return NotFound();
            }
            double[] k = Database.AddOrder(p);
            return Ok(k);
        }

        public IHttpActionResult Get()
        {
            var t = Database.getOrder();
            return Ok(t);

        }

    }
}
