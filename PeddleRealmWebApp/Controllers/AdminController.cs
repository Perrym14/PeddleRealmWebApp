using PeddleRealmWebApp.Models;
using PeddleRealmWebApp.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

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
    }
}