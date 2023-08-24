using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class CustomerDeletionServiceFactory
    {
        private readonly VaagoProjectEntities1 _db;

        public CustomerDeletionServiceFactory(VaagoProjectEntities1 db)
        {
            _db = db;
        }

        public ICustomerDeletionService CreateCustomerDeletionService()
        {
            return new CustomerDeletionService(_db);
        }
    }

}