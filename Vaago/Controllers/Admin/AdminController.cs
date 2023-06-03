﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class AdminController : Controller
    {
        VaagoProjectEntities1 DB = new VaagoProjectEntities1();
        // GET: AdminAuth
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(Login admin)
        {
            if (ModelState.IsValid)
            {

                var userDetails = DB.Accounts.Where(m => m.email == admin.Email && m.pass == admin.Password && m.account_type == 1).FirstOrDefault();
                if (userDetails == null)
                {
                    Console.WriteLine("Error", "Email/Password is invalidd.");
                    return RedirectToAction("index");
                }
                else
                {
                    Session["Admin"] = userDetails;
                    return RedirectToAction("Index", "Dashboard");
                }
            }
            else
            {
                Console.WriteLine("Error", "Enter email or password.");
                return RedirectToAction("index");
            }
        }



        public ActionResult Logout_Account()
        {
            Session.Abandon();
            return RedirectToAction("index", "Home");
        }


    }
}