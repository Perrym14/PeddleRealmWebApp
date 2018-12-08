using PeddleRealmWebApp.Models;
using System.Collections.Generic;

namespace PeddleRealmWebApp.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}