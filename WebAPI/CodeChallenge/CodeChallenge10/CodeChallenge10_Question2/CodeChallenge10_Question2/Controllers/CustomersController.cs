using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeChallenge10_Question2.Models;

namespace CodeChallenge10_Question2.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private NorthwindDBEntities db = new NorthwindDBEntities();

        // GET api/customers/bycountry/USA
        [HttpGet]
        [Route("bycountry/{country}")]
        public IHttpActionResult GetCustomersByCountry(string country)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(country))
                    return BadRequest("Country parameter is required.");

                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                // Option A: If you added a Function Import in the EDMX:
                //var customers = GetCustomersByCountry(country).ToList();

                // Option B: If function import wasn't created, fallback:
                 var customers = db.Database.SqlQuery<Customer>("EXEC GetCustomersByCountry @p0", country).ToList();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                // TODO: log ex
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
