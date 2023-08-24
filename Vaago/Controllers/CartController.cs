using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRetrievalStrategy _cartRetrievalStrategy;
        private readonly VaagoProjectEntities1 _context;

        public CartController(ICartRetrievalStrategy cartRetrievalStrategy, VaagoProjectEntities1 context)
        {
            _cartRetrievalStrategy = cartRetrievalStrategy;
            _context = context;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cur = Session["userID"];
            var num = Session["non-userID"];
            int accountId = 0;

            if (cur != null)
            {
                accountId = ((Vaago.Models.Account)cur).account_ID;
            }
            else if (num != null)
            {
                accountId = Convert.ToInt32(num);
            }

            IEnumerable<CartItem> cartNitem = _cartRetrievalStrategy.GetCartItems(accountId); 

            return View(cartNitem);
        }

        [HttpPost]
        public ActionResult UpdateCart(string totalBill)
        {
            var cur = Session["userID"];
            int finalVal;
            if (cur != null)
            {
                var accID = ((Vaago.Models.Account)cur).account_ID;
                finalVal = accID;
            }
            else
            {
                var num = Session["non-userID"];
                int innum = Convert.ToInt32(num);
                finalVal = innum;
            }

            List<Cart> obj = _context.Carts.Where(x => x.account_ID == finalVal).ToList();
            foreach (var item in obj)
            {
                item.totalAmount = Convert.ToInt32(totalBill);

            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Checkout");
        }
    }
}
