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
        private VaagoProjectEntities1 DB = new VaagoProjectEntities1();
        private readonly CustomerDeletionServiceFactory _deletionServiceFactory;

        public AdminCustomersController()
        {
            _deletionServiceFactory = new CustomerDeletionServiceFactory(DB);
        }

        public ActionResult Index()
        {
            List<Account> customersList = DB.Accounts.Where(c => c.account_type == 2).ToList();
            return View(customersList);
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int[] customerID)
        {
            try
            {
                var customers = DB.Accounts.Where(c => customerID.Contains(c.account_ID) && c.account_type == 2).ToList();

                if (customers.Count > 0)
                {
                    var deletionService = _deletionServiceFactory.CreateCustomerDeletionService();
                    deletionService.DeleteCustomers(customerID);

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Customers not found." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, message = "An error occurred while deleting the customers." });
            }
        }
    }
}
