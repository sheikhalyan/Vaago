using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class AdminCustomersController : Controller
    {
        VaagoProjectEntities1 DB = new VaagoProjectEntities1();
        // GET: AdminCustomers
        public ActionResult Index()
        {
            List<Account> customersList = DB.Accounts.Where(x => x.account_type == 2).ToList();
            return View("~/Views/Admin/AdminCustomers.cshtml", customersList);
            //}
        }
        [HttpPost]
        public ActionResult DeleteCustomer(int[] customerID)
        {
            try
            {
                var customers = DB.Accounts.Where(c => customerID.Contains(c.account_ID) && c.account_type == 2).ToList();

                if (customers.Count > 0)
                {
                    // Delete the customers
                    DB.Accounts.RemoveRange(customers);
                    DB.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Customers not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the customers." });
            }
        }


    }
}