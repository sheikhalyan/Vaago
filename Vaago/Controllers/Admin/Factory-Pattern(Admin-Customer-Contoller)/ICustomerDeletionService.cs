using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaago.Controllers.Admin
{
    public interface ICustomerDeletionService
    {
        void DeleteCustomers(int[] customerIDs);
    }

}
