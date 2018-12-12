using FluentAssertions;
using NUnit.Framework;
using PeddleRealm.IntegrationTests.Extensions;
using PeddleRealmWebApp.Controllers;
using PeddleRealmWebApp.Models;
using System.Linq;

namespace PeddleRealm.IntegrationTests.Controllers.ShoppingCart
{
    [TestFixture]
    public class UnitTest1
    {
        private ShoppingCartController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new ShoppingCartController();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
        [Test, Isolated]
        public void AddToCart_WhenCalled_ShouldAddItemToCart()
        {
            //Arrange
            var user = _context.Users.SingleOrDefault(u => u.UserName == "JohnJ");
            _controller.MockCurrentUser(user.Id, user.UserName);

            var itemType = GetItemType();

            var item = CreateNewItem(itemType);
            _context.Items.Add(item);
            _context.SaveChanges();

            //Act - Need to Mock HttpContext
            var result = _controller.AddToCart(item.Id);

            //Assert
            _context.Carts.Should().HaveCount(1);

        }

        public Item CreateNewItem(ItemType itemType)
        {
            var item = new Item
            {
                Id = 1,
                Description = "test",
                IsDeleted = false,
                ItemPhoto = "-",
                ItemType = itemType,
                Name = "Blah",
                Price = 19.7m
            };
            return item;
        }

        private ItemType GetItemType()
        {
            var itemType = _context.ItemTypes.SingleOrDefault(i => i.Id == 1);
            return itemType;
        }

    }
}
