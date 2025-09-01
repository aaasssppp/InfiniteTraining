using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeChallenge10_Question2.Models;

namespace CodeChallenge10_Question2.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private NorthwindDBEntities db = new NorthwindDBEntities();

        // GET api/orders/employee/5
        [HttpGet]
        [Route("employee/{employeeId:int}")]
        public IHttpActionResult GetOrdersByEmployee(int employeeId)
        {
            try
            {
                // avoid proxy/loop serialization problems
                db.Configuration.ProxyCreationEnabled = false;
                db.Configuration.LazyLoadingEnabled = false;

                var employee = db.Employees.FirstOrDefault(e => e.EmployeeID == employeeId);
                if (employee == null)
                    return NotFound();

                // Optional: verify it's Steven Buchanan (case-insensitive)
                if (!string.Equals(employee.FirstName, "Steven", StringComparison.OrdinalIgnoreCase)
                    || !string.Equals(employee.LastName, "Buchanan", StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest("EmployeeId does not match Steven Buchanan.");
                }

                var orders = db.Orders
                               .Where(o => o.EmployeeID == employeeId)
                               .Select(o => new
                               {
                                   o.OrderID,
                                   o.OrderDate,
                                   o.RequiredDate,
                                   o.ShipName,
                                   o.ShipCity,
                                   o.ShipCountry,
                                   o.CustomerID
                               })
                               .ToList();

                // Return OK with empty array if none — that's a valid response
                return Ok(orders);
            }
            catch (Exception ex)
            {
                // For per-action strong logging: log ex here
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
