using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaago.Models
{//Concrete Command Class
    public class PlaceOrderCommand : IOrderCommand
    {
        private readonly VaagoProjectEntities1 _context;

        public PlaceOrderCommand(VaagoProjectEntities1 context)
        {
            _context = context;
        }

        public void Execute(int accountId, string checkoutName, string checkoutEmail, int itemsCount, string checkoutPhone, string checkoutCity, string checkoutAddress, string checkoutMessage, int totalBill)
        {
            DateTime now = DateTime.Now;

            var user = _context.Accounts.FirstOrDefault(x => x.account_ID == accountId);
            if (user != null)
            {
                user.name = checkoutName;
                user.phone = checkoutPhone;
                user.location = checkoutAddress;
                _context.SaveChanges();
            }

            Order new_order = new Order();
            new_order.CustomerID = accountId;
            new_order.orderTime = now.ToLongTimeString();
            new_order.orderDate = now.ToLongDateString();
            new_order.orderStatus = "Pending";
            new_order.numberOfItems = itemsCount.ToString();
            new_order.Amount = totalBill.ToString();
            new_order.deliveryCharges = "50";
            new_order.City = checkoutCity;
            new_order.notes = checkoutMessage;

            _context.Orders.Add(new_order);
            _context.SaveChanges();

            int orderId = new_order.orderID;

            List<Cart> items = _context.Carts.Where(x => x.account_ID == accountId).ToList();
            foreach (var item in items)
            {
                Order_Details det = new Order_Details();
                det.orderID = orderId;
                det.itemID = item.itemID;
                det.itemQuantity = 1;
                _context.Order_Details.Add(det);
                _context.Carts.Remove(item);
            }

            _context.SaveChanges();
        }
    }

}
