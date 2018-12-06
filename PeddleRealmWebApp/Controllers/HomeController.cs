using PeddleRealmWebApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var item = _context.Items.Where(i => !i.IsDeleted).ToList();
            return View(item);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}