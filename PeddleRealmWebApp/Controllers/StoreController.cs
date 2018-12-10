﻿using PeddleRealmWebApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Controllers
{
    public class StoreController : Controller
    {
        private ApplicationDbContext _context;

        public StoreController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        // GET: Store/Browse?itemType=Food
        public ActionResult Browse(string itemType)
        {
            var itemTypeModel = _context.ItemTypes.Include("Items").SingleOrDefault(g => g.Name == itemType);
            return View(itemTypeModel);
        }

        [ChildActionOnly]
        public ActionResult ItemTypeMenu()
        {
            var itemTypes = _context.ItemTypes.ToList();
            return PartialView(itemTypes);
        }
    }
}