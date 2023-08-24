using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vaago.Models;
using Vaago.ViewModels;

namespace Vaago.Controllers.Admin
{
    public class ChefsController : ApiController
    {
        private readonly VaagoProjectEntities1 db = new VaagoProjectEntities1();

        // GET: api/chefs
        public IHttpActionResult GetChefs()
        {
            try
            {
                var chefs = db.Chefs.ToList();
                return Ok(chefs);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return InternalServerError(ex);
            }
        }

        // GET: api/chefs/5
        public IHttpActionResult GetChef(int id)
        {
            try
            {
                var chef = db.Chefs.FirstOrDefault(c => c.Chef_ID == id);
                if (chef == null)
                    return NotFound();

                return Ok(chef);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
