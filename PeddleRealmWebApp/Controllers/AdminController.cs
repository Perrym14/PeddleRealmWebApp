﻿using PeddleRealmWebApp.Models;
using PeddleRealmWebApp.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin
        public ViewResult Index()
        {
            var itemsForSale = _context.Items.Where(i => !i.IsDeleted).ToList();
            return View(itemsForSale);


        }

        public ActionResult Create()
        {
            var viewModel = new ItemViewModel
            {
                ItemTypes = _context.ItemTypes.ToList(),
                Heading = "Add item for sell."
            };
            return View("ItemForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var item = _context.Items.SingleOrDefault(i => i.Id == id);

            var viewModel = new ItemViewModel
            {
                Id = item.Id,
                ItemTypes = _context.ItemTypes.ToList(),
                Heading = "Add item for sell.",
                Description = item.Description,
                Name = item.Name,
                ItemType = item.ItemType.Id,
                Price = item.Price
            };
            return View("ItemForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemViewModel viewModel, HttpPostedFileBase uploadedFile)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ItemTypes = _context.ItemTypes.ToList();
                return View("ItemForm", viewModel);
            }

            {
                var fileName = UploadPhoto(uploadedFile);
                var item = new Item
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    ItemTypeId = viewModel.ItemType,
                    Price = viewModel.Price,
                    ItemPhoto = fileName

                };


                _context.Items.Add(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Admin");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ItemTypes = _context.ItemTypes.ToList();
                return View("ItemForm", viewModel);
            }

            var item = _context.Items.SingleOrDefault(i => i.Id == viewModel.Id);
            item.Name = viewModel.Name;
            item.Description = viewModel.Description;
            item.ItemTypeId = viewModel.ItemType;
            item.Price = viewModel.Price;
            item.ItemPhoto = item.ItemPhoto;
            _context.SaveChanges();


            return RedirectToAction("Index", "Admin");
        }

        private string UploadPhoto(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);

                //Add randomly generated ID to avoid duplicates.
                var random = Guid.NewGuid() + fileName;
                var path = Path.Combine(
                    System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/UserPhotos"), random);

                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/UserPhotos")))
                {
                    Directory.CreateDirectory(
                        System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/UserPhotos"));
                }
                file.SaveAs(path);


                //var awsS3Uploader = new AmazonS3Uploader(random, path);
                // awsS3Uploader.UploadFile(); - Causes null exception. Fix later
                return random;
            }

            return "nofile.png";
        }

    }
}
