﻿using System;
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
        VaagoProjectEntities DB = new VaagoProjectEntities();

        // GET: Menu
        public ActionResult Index()
        {
            List<Menu> menuList = DB.Menus.ToList();
            return View("~/Views/Admin/Menu.cshtml", menuList);
            //}
        }


        // GET: Add Item
        public ActionResult AddItemView()
        {
            return View("~/Views/Admin/Menu_Insert.cshtml");
        }
       // itemImgpath
        [HttpPost]
        public ActionResult Add(Menu item)
        {
            if (item != null && item.itemName != null)
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
                            item.itemImgpath = "/images/" + filename;
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
    }
}