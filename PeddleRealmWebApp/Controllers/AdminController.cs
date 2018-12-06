using PeddleRealmWebApp.Models;
using System.Linq;
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
            var itemsForSale = _context.Items.Where(i => !i.IsDeleted).ToList();
            return View(itemsForSale);
        }

    }
}
