using PeddleRealmWebApp.Models;
using PeddleRealmWebApp.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _context;

        public ShoppingCartController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            return View(viewModel);
        }
        // GET: /Store/AddToCart/2
        public ActionResult AddToCart(int id)
        {
            var addedItem = _context.Items
                .SingleOrDefault(i => i.Id == id);

            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedItem);
            return RedirectToAction("Index", "ShoppingCart");
        }

        //AJAX: /ShoppingCart/RemoveFromCart/2
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            string itemName = GetItemNameFromCartByRecordId(id);

            //Set new item count after item removal.
            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(itemName) +
                          " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        private string GetItemNameFromCartByRecordId(int id)
        {
            return _context.Carts.SingleOrDefault(
                i => i.RecordId == id).Item.Name;
        }

        //Get: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}