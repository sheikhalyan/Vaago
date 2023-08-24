using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{//OBSERVER CLASS ITSELF
 //It is interested in receiving notifications about changes in order statuses.
    public class AdminOrdersController : Controller, IObserver
    {
        private VaagoProjectEntities1 DB = new VaagoProjectEntities1();
        private OrderSubject _orderSubject = new OrderSubject();

        public AdminOrdersController()
        {
            _orderSubject.Attach(this);//means that the controller is now registered to receive notifications from the OrderSubject
        }

        // GET: AdminOrders
        public ActionResult Index()
        {
            List<Order> ordersList = DB.Orders.ToList();

            // Check if there's a success message in TempData
            ViewBag.SuccessMessage = TempData["SuccessMessage"];

            return View("~/Views/Admin/AdminOrders.cshtml", ordersList);
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
                var obj = DB.Orders.Where(x => x.orderID == orderID).FirstOrDefault();
                if (obj != null)
                {
                    obj.orderStatus = orderStatus;
                    DB.SaveChanges();

                    // Notify observers about the status update
                    _orderSubject.Notify(obj, orderStatus);

                    // Set the success message in TempData
                    TempData["SuccessMessage"] = $"Order {orderID} status changed to {orderStatus}";
                }
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

        // Implement the IObserver method
        public void Update(Order order, string status)
        {
            // Logic to handle the notification when an order status is updated
           
            TempData["SuccessMessage"] = $"Order {order.orderID} status changed to {status}";
        }
    }
}
