using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PeddleRealmWebApp.Models
{
    public partial class ShoppingCart
    {
        private ApplicationDbContext _context;
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public ShoppingCart()
        {
            _context = new ApplicationDbContext();
        }

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        //Helper method to simplify shopping cart calls.
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Item item)
        {
            var cartItem = _context.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                     && c.ItemId == item.Id);

            if (cartItem == null)
            {
                cartItem = new Cart
                {
                    ItemId = item.Id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _context.Carts.Add(cartItem);
            }
            else
            {
                //If item does exist in cart,
                //add one to quantity
                cartItem.Count++;
            }

            _context.SaveChanges();
        }

        public int RemoveFromCart(int id)
        {
            var cartItem = _context.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                     && c.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _context.Carts.Remove(cartItem);
                }

                _context.SaveChanges();
            }

            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = _context.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _context.Carts.Remove(cartItem);
            }

            _context.SaveChanges();
        }

        public List<Cart> GetCartItems()
        {
            return _context.Carts.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in _context.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            //Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            //Multiply item's price by count of that item
            decimal? total = (from cartItems in _context.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                                     cartItems.Item.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            //Iterate over the items in the cart,
            //adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ItemId = item.ItemId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Item.Price,
                    Quantity = item.Count
                };
                //Set the order total of the shopping cart
                orderTotal += (item.Count * item.Item.Price);

                _context.OrderDetails.Add(orderDetail);
            }
            // Set the orders total to the orderTotal count
            order.Total = orderTotal;
            _context.SaveChanges();
            EmptyCart();
            //Return the OrderId as the confirmation number
            return order.OrderId;
        }

        //Using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                //If user is not logged in, give temp id.
                else
                {
                    var tempCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }

            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = _context.Carts.Where(c => c.CartId == ShoppingCartId);

            foreach (var item in shoppingCart)
            {
                item.CartId = userName;
            }

            _context.SaveChanges();
        }
    }
}