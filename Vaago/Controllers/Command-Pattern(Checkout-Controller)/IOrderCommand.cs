using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaago.Models
{
    public interface IOrderCommand
    {
        void Execute(int accountId, string checkoutName, string checkoutEmail, int itemsCount, string checkoutPhone, string checkoutCity, string checkoutAddress, string checkoutMessage, int totalBill);
    }
}
