using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vaago.Models
{
    public class CartRepository : ICartRepository
    {
        private readonly VaagoProjectEntities1 _context;

        public CartRepository(VaagoProjectEntities1 context)
        {
            _context = context;
        }

        public void AddToCart(Cart cartItem)
        {
            _context.Carts.Add(cartItem);
            _context.SaveChanges();
        }
    }
}
