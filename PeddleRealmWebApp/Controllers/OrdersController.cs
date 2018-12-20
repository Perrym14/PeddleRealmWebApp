using Microsoft.AspNet.Identity;
using PeddleRealmWebApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;

        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Orders
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var orders = _context.Orders
                .Where(o => o.BuyerId == userId)
                .ToList();
            return View(orders);
        }
    }
}