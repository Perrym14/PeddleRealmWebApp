using PeddleRealmWebApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext _context;

        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Details(int id)
        {
            var item = _context.Items.SingleOrDefault(i => i.Id == id);
            return View("ItemDetails", item);
        }




    }
}