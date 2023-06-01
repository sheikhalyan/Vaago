using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class AdminMenuController : Controller
    {
        private VaagoProjectEntities DB = new VaagoProjectEntities();

        // GET: Menu
        public ActionResult Index()
        {
            List<Menu> menuList = DB.Menus.ToList();
            return View("~/Views/Admin/Menu.cshtml", menuList);
        }


        // GET: Add Item
        public ActionResult AddItemView()
        {
            return View("~/Views/Admin/Menu_Insert.cshtml");
        }

        [HttpPost]
        public ActionResult Add(Menu item)
        {
            if (item != null && !string.IsNullOrEmpty(item.itemName))
            {
                if (item.imgFile != null && item.imgFile.ContentLength > 0)
                {
                    string filename = Path.GetFileNameWithoutExtension(item.imgFile.FileName);
                    string extension = Path.GetExtension(item.imgFile.FileName);

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".jpeg")
                    {
                        if (item.imgFile.ContentLength <= 1000000)
                        {
                            filename = filename + extension;
                            item.itemImgpath = "/images/" + filename; // I have Put ~ here
                            filename = Path.Combine(Server.MapPath("~/images/"), filename);
                            item.imgFile.SaveAs(filename);

                            DB.Menus.Add(item);
                            int a = DB.SaveChanges();

                            if (a > 0)
                            {
                                ViewBag.Message = "<script>alert('Record Inserted!!')</script>";
                                ModelState.Clear();
                            }
                            else
                            {
                                ViewBag.Message = "<script>alert('Record Not Inserted!!')</script>";
                            }
                        }
                        else
                        {
                            ViewBag.SizeMessage = "<script>alert('Size not supported!!')</script>";
                        }
                    }
                    else
                    {
                        ViewBag.ExtensionMessage = "<script>alert('Extension not supported!!')</script>";
                    }
                }
            }

            return RedirectToAction("AddItemView");
        }

        [HttpPost]
        public ActionResult DeleteSelected(List<int> items)
        {
            if (items != null && items.Count > 0)
            {
                try
                {
                    // Delete the selected items
                    var itemsToDelete = DB.Menus.Where(x => items.Contains(x.itemID)).ToList();

                    foreach (var item in itemsToDelete)
                    {
                        // Delete the associated image file if it exists
                        if (!string.IsNullOrEmpty(item.itemImgpath) || System.IO.File.Exists(Server.MapPath(item.itemImgpath)))
                        {
                            System.IO.File.Delete(Server.MapPath(item.itemImgpath));  //------> Not going here
                        }

                        DB.Menus.Remove(item);
                    }

                    DB.SaveChanges();

                    return Json(new { success = true });  //-------> and here
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "An error occurred while deleting menu items." }); //--------> After this it is going here
                }
            }
            else
            {
                // No items selected, return a message to choose items to delete
                return Json(new { success = false, message = "Please select items to delete." });
            }
        }

    }
}
