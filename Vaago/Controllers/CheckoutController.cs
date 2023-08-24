using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers
{//Client & (Invoker)
    public class CheckoutController : Controller
    {
        private readonly IOrderCommand _orderCommand;
        private readonly VaagoProjectEntities1 _context;//Reciever

        public CheckoutController(IOrderCommand orderCommand, VaagoProjectEntities1 context)
        {
            _orderCommand = orderCommand;
            _context = context;
        }

        // GET: Checkout
        public ActionResult Index()
        {
            var cur = Session["userID"];
            if (cur != null)
            {
                var accID = ((Vaago.Models.Account)cur).account_ID;

                var checkoutDetails = from ca in _context.Carts
                                      join m in _context.Menus on ca.itemID equals m.itemID
                                      join u in _context.Accounts on ca.account_ID equals u.account_ID
                                      where ca.account_ID == accID
                                      select new CheckoutModel
                                      {
                                          menuItem = m,
                                          cartItem = ca,
                                          user = u,
                                      };

                return View(checkoutDetails);
            }

            return RedirectToAction("Index", "Authentication");
        }
        //INVOKING
        public ActionResult Place_Order(string checkoutName, string checkoutEmail, int itemsCount, string checkoutPhone, string checkoutCity, string checkoutAddress, string checkoutMessage, int totalBill)
        {
            var cur = Session["userID"];
            int accID = ((Vaago.Models.Account)cur)?.account_ID ?? 0;
            //Exucting the command
            _orderCommand.Execute(accID, checkoutName, checkoutEmail, itemsCount, checkoutPhone, checkoutCity, checkoutAddress, checkoutMessage, totalBill);

            return RedirectToAction("Index", "Home");
        }
    }
}
