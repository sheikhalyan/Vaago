using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaago.Models
{
    public class LoggedInCartRetrievalStrategy : ICartRetrievalStrategy
    {
        private readonly VaagoProjectEntities1 _context;

        public LoggedInCartRetrievalStrategy(VaagoProjectEntities1 context)
        {
            _context = context;
        }

        public IEnumerable<CartItem> GetCartItems(int accountId)
        {
            var cartNitem = from ca in _context.Carts
                            join m in _context.Menus on ca.itemID equals m.itemID
                            where ca.account_ID == accountId
                            select new CartItem
                            {
                                menuItem = m,
                                cartItem = ca
                            };

            return cartNitem;
        }
    }

    public class NonLoggedInCartRetrievalStrategy : ICartRetrievalStrategy
    {
        private readonly VaagoProjectEntities1 _context;

        public NonLoggedInCartRetrievalStrategy(VaagoProjectEntities1 context)
        {
            _context = context;
        }

        public IEnumerable<CartItem> GetCartItems(int accountId)
        {
            var cartNitem = from ca in _context.Carts
                            join m in _context.Menus on ca.itemID equals m.itemID
                            where ca.account_ID == accountId
                            select new CartItem
                            {
                                menuItem = m,
                                cartItem = ca
                            };

            return cartNitem;
        }
    }


}