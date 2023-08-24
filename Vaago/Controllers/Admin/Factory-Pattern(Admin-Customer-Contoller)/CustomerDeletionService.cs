using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class CustomerDeletionService : ICustomerDeletionService
    {
        private readonly VaagoProjectEntities1 _db;

        public CustomerDeletionService(VaagoProjectEntities1 db)
        {
            _db = db;
        }

        public void DeleteCustomers(int[] customerIDs)
        {
            var customers = _db.Accounts.Where(c => customerIDs.Contains(c.account_ID) && c.account_type == 2).ToList();
            if (customers.Count > 0)
            {
                _db.Accounts.RemoveRange(customers);
                _db.SaveChanges();
            }
        }
    }

}