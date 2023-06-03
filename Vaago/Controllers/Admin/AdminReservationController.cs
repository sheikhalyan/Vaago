using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class AdminReservationController : Controller
    {
        VaagoProjectEntities1 DB = new VaagoProjectEntities1();
        // GET: AdminReservation
        public ActionResult Index()
        { 
            List<Reservation> reservationList = DB.Reservations.ToList();

            return View("~/Views/AdminReservation/AdminReservation.cshtml", reservationList);
            
            //return View();
        }

        [HttpPost]
        public ActionResult DeleteReservations(List<int> items)
        {
            if (items != null)
            {
                try
                {
                    var reservationsToDelete = DB.Reservations.Where(r => items.Contains(r.reserveID)).ToList();
                    DB.Reservations.RemoveRange(reservationsToDelete);
                    DB.SaveChanges();
                    return Json(new { success = true });
                }

                catch(Exception ex)
                {
                    return Json(new { success = false, message = "An error occurred while deleting menu items." });
                }
                
            }
            else
            {
                // No items selected, return a message to choose items to delete
                return Json(new { success = false, message = "Please select items to delete." });
            }

            //return RedirectToAction("Index");

            
        }

    }
}