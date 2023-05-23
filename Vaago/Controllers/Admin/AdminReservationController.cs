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
        VaagoProjectEntities DB = new VaagoProjectEntities();
        // GET: AdminReservation
        public ActionResult Index()
        { 
            List<Reservation> reservationList = DB.Reservations.ToList();

            return View("~/Views/AdminReservation/AdminReservation.cshtml", reservationList);
            
            //return View();
        }
    }
}