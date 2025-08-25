using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeChallenge9_Question1.Models;

namespace CodeChallenge9_Question1.Controllers
{
    public class CodeController : Controller
    {
        // GET: Code
        public ActionResult Index()
        {
            return View();
        }
        private readonly NorthwindDBEntities db = new NorthwindDBEntities();

        // 1) All customers residing in Germany
        public ActionResult GermanyCustomers()
        {
            var customersInGermany = db.Customers
                                       .Where(c => c.Country == "Germany")
                                       .OrderBy(c => c.CompanyName)
                                       .ToList();
            return View(customersInGermany);
        }

        // 2) Customer details for the given orderId (default 10248)
        public ActionResult CustomerByOrder(int id = 10248)
        {
            var customer = db.Orders
                             .Where(o => o.OrderID == id)
                             .Select(o => o.Customer)
                             .FirstOrDefault();

            if (customer == null)
                return HttpNotFound($"No customer found for order {id}.");

            ViewBag.OrderId = id;
            return View(customer); 
        }
    }
}
