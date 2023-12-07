using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaago.Models
{
    public interface IMenuRepository
    {
        List<Menu> GetAllMenus();
    }

    public interface ICartRepository
    {
        void AddToCart(Cart cartItem);
    }
}

