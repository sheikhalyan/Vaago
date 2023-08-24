using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class AdminChefController : Controller
    {
        VaagoProjectEntities1 DB = new VaagoProjectEntities1();
        // GET: AdminChef
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53521/api/");

                // Send HTTP request to the API to retrieve all chefs
                var responseTask = client.GetAsync("chefs");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var chefs = result.Content.ReadAsAsync<List<Chef>>().Result;
                    return View(chefs);
                }
                else
                {
                    // Log or handle the error
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }

            return View(new List<Chef>());
        }



        // GET: Chefs/Details/5
        public ActionResult Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53521/api/");

                // Send HTTP request to the API to retrieve a specific chef by ID
                var responseTask = client.GetAsync($"chefs/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var chef = result.Content.ReadAsAsync<Chef>().Result;
                    return View(chef);
                }
                else if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return HttpNotFound();
                }
                else
                {
                    // Log or handle the error
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
                }
            }

            return View();
        }





































        //public ActionResult DeleteChef(List<int> chef)
        //{
        //    if (chef != null)
        //    {
        //        try
        //        {
        //            var chefToDelete = DB.Chefs.Where(c => chef.Contains(c.Chef_ID)).ToList();
        //            DB.Chefs.RemoveRange(chefToDelete);
        //            DB.SaveChanges();
        //            return Json(new { success = true });
        //        }

        //        catch (Exception ex)
        //        {
        //            return Json(new { success = false, message = "An error occurred while deleting Chefs Request." });
        //        }

        //    }
        //    else
        //    {
        //        No items selected, return a message to choose items to delete
        //        return Json(new { success = false, message = "Please select item to delete." });
        //    }

            //return RedirectToAction("Index");


        }



        //List<Chef> reservationList = DB.Chefs.ToList();

        //return View("~/Views/Admin/Adminchef.cshtml", reservationList);

        //return View();
    }

