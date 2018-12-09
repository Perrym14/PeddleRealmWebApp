using PeddleRealmWebApp.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private ApplicationDbContext _context;
        private const string PromoCode = "FREE";

        public CheckoutController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Checkout
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        // POST: Checkout/AdressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection info)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(info["PromoCode"], PromoCode,
                        StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        // GET: Checkout/Complete
        public ActionResult Complete(int id)
        {
            //Validate customer owns this order.
            bool isValid = _context.Orders.Any(o => o.OrderId == id &&
                                                    o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}