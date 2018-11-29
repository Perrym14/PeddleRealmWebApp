﻿using PeddleRealmWebApp.Models;
using PeddleRealmWebApp.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {
            var viewModel = new ItemViewModel
            {
                ItemTypes = _context.ItemTypes.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemViewModel viewModel, HttpPostedFileBase uploadedFile)
        {

            {
                var fileName = UploadPhoto(uploadedFile);
                var itemType = _context.ItemTypes.SingleOrDefault(i => i.Id == viewModel.ItemType);

                var item = new Item
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    ItemType = itemType,
                    Price = viewModel.Price,
                    ItemPhoto = fileName

                };


                _context.Items.Add(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Admin");
        }

        public string UploadPhoto(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var random = Guid.NewGuid() + fileName;
                var path = Path.Combine(
                    System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Images"), random);

                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Images")))
                {
                    Directory.CreateDirectory(
                        System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Images/UserPhotos"));
                }
                file.SaveAs(path);


                //var awsS3Uploader = new AmazonS3Uploader(random, path);
                // awsS3Uploader.UploadFile();
                return random;
            }

            return "nofile.png";
        }

    }
}
