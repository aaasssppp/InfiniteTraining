using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeChallenge10_Question1.Models;

namespace CodeChallenge10_Question1.Controllers
{
    [RoutePrefix("api/Country")]
    public class CountryController : ApiController
    { 
        static List<Country> countries = new List<Country>()
        {
            new Country { ID = 1, CountryName = "India", Capital = "New Delhi" },
            new Country { ID = 2, CountryName = "China", Capital = "Beijing" },
        };

        [HttpGet]
        [Route("All")]
        public IEnumerable<Country> GetAll()
        {
            return countries;
        }

        [HttpGet]
        [Route("ById")]
        public IHttpActionResult GetById(int id)
        {
            var item = countries.FirstOrDefault(c => c.ID == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage AddCountry([FromBody] Country country)
        {
            if (country == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Country payload is required.");

            if (countries.Any(c => c.ID == country.ID))
                return Request.CreateResponse(HttpStatusCode.Conflict, "Country with this ID already exists.");

            countries.Add(country);
            return Request.CreateResponse(HttpStatusCode.Created, country);
        }

        [HttpPut]
        [Route("Update")]
        public HttpResponseMessage UpdateCountry([FromBody] Country country)
        {
            if (country == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Country payload is required.");

            var existing = countries.FirstOrDefault(c => c.ID == country.ID);
            if (existing == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Country not found.");

            existing.CountryName = country.CountryName;
            existing.Capital = country.Capital;

            return Request.CreateResponse(HttpStatusCode.OK, existing);
        }

        [HttpDelete]
        [Route("Delete")]
        public HttpResponseMessage DeleteCountry(int id)
        {
            var existing = countries.FirstOrDefault(c => c.ID == id);
            if (existing == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Country not found.");

            countries.Remove(existing);
            return Request.CreateResponse(HttpStatusCode.OK, countries);
        }
    }
}
