using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class AdminOrdersController : Controller
    {
        VaagoProjectEntities1 DB = new VaagoProjectEntities1();
        // GET: AdminOrders
        public ActionResult Index()
        {
            List<Order> ordersList = DB.Orders.ToList();

            return View("~/Views/Admin/AdminOrders.cshtml", ordersList);
            //}
        }

        public ActionResult ViewOrder(int orderID)
        {
            var orderHistory = from ol in DB.Orders
                               join od in DB.Order_Details on ol.orderID equals od.orderID
                               join cust in DB.Accounts on ol.CustomerID equals cust.account_ID
                               join item in DB.Menus on od.itemID equals item.itemID
                               where ol.orderID == orderID
                               select new ViewModel
                               {
                                   order = ol,
                                   order_Details = od,
                                   customer = cust,
                                   menuItem = item
                               };

            return View("~/Views/Admin/ViewOrder.cshtml", orderHistory);
        }

        public ActionResult UpdateStatus(int orderID, string orderStatus)
        {
            var cur = Session["Admin"];
            if (ModelState.IsValid)
            {
                var obj = DB.Orders.Where(x => x.orderID == orderID).First<Order>();

                obj.orderStatus = orderStatus ;
                DB.SaveChanges();
            }

            return RedirectToAction("Index", "AdminOrders");
        }
        [HttpPost]
        public ActionResult DeleteOrder(int[] orderID)
        {
            try
            {
                foreach (var id in orderID)
                {
                    var order = DB.Orders.FirstOrDefault(o => o.orderID == id);
                    if (order != null)
                    {
                        DB.Orders.Remove(order);
                    }
                }

                DB.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting menu items." });
            }
        }
        /*[HttpPost]
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
        }*/



    }
}