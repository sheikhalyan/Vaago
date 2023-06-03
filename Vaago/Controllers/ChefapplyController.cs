using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;
using Vaago.ViewModels;

namespace Vaago.Controllers
{
    public class ChefapplyController : Controller
    {
       
        // GET: Chefapply
        VaagoProjectEntities1 DB = new VaagoProjectEntities1();
        // GET: Reservations
        public ActionResult Index()
        {
            return View();
        }


        private readonly string apiBaseUrl = "http://localhost:53521/api/Chefreq/"; // Replace with your API endpoint

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Chef chef)
        {
            if (!ModelState.IsValid)
            {
                return View(chef);
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);

                // Send HTTP POST request to the API
                HttpResponseMessage response = client.PostAsJsonAsync("PostChef", chef).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Data successfully posted
                    return RedirectToAction("Detail");
                }
                else
                {
                    // Failed to post data
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact the administrator.");
                    return View(chef);
                }
            }
        }

        public ActionResult Detail()
        {
            return View();
        }



















        //[System.Web.Mvc.HttpPost]

        //public ActionResult create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult create(ChefViewModel chef)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:53521/api/");

        //        //HTTP POST
        //        var postTask = client.PostAsJsonAsync<ChefViewModel>("Chefreq", chef);
        //        postTask.Wait();

        //        var result = postTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //    }

        //    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

        //    return View(chef);

        //}

    }
}