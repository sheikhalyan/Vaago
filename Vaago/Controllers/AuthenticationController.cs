using System;
using System.Linq;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers
{
    public class AuthenticationController : Controller
    {
        private static AuthenticationController _instance;
        private static readonly object lockObject = new object();

        private VaagoProjectEntities1 DB;

        // Private constructor to enforce the Singleton pattern
        public AuthenticationController()
        {
            DB = new VaagoProjectEntities1();
        }

        // Singleton instance property
        public static AuthenticationController Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new AuthenticationController();
                    }
                    return _instance;
                }
            }
        }

        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login_Account(Login user)
        {
            if (ModelState.IsValid)
            {
                var userDetails = DB.Accounts.Where(m => m.email == user.Email && m.pass == user.Password && m.account_type == 2).FirstOrDefault();
                if (userDetails == null)
                {
                    TempData["Msg"] = "Email/Password is invalid!";
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["userID"] = userDetails;
                    return RedirectToAction("Index", "UProfile");
                }
            }
            else
            {
                TempData["Msg"] = "Email/Password is invalid!";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout_Account()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Authentication");
        }

        // GET: Authentication
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register_Account(Account user)
        {
            if (ModelState.IsValid)
            {
                Account obj = new Account
                {
                    account_type = 2,
                    name = user.name,
                    email = user.email,
                    pass = user.pass,
                    phone = Equals(user.phone) ? user.phone : null,
                    location = Equals(user.location) ? user.location : null
                };

                DB.Accounts.Add(obj);
                DB.SaveChanges();

                Session["userID"] = obj;
                return RedirectToAction("Index", "UProfile");
            }
            else
            {
                Console.WriteLine("Error", "Enter email or password.");
                return RedirectToAction("CreateAccount");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DB != null)
                {
                    DB.Dispose();
                    DB = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
