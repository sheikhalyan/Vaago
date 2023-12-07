using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaago.Models
{
    public interface ICartRetrievalStrategy
    {
        IEnumerable<CartItem> GetCartItems(int accountId);
    }


}
