using FluentAssertions;
using NUnit.Framework;
using PeddleRealm.IntegrationTests.Extensions;
using PeddleRealmWebApp.Models;
using PeddleRealmWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace PeddleRealm.IntegrationTests.AdminController
{
    [TestFixture]
    public class UnitTest1
    {
        private PeddleRealmWebApp.Controllers.AdminController _controller;
        private ApplicationDbContext _context;

        public UnitTest1()
        {
            _context = new ApplicationDbContext();
        }

        [SetUp]
        public void SetUp()
        {
            _controller = new PeddleRealmWebApp.Controllers.AdminController();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void AdminIndex_WhenCalled_ShouldReturnListOfItemsNotDeleted()
        {
            //Arrange
            var user = _context.Users.SingleOrDefault(u => u.UserName == "MarkT");
            _controller.MockCurrentUser(user.Id, user.UserName);

            //Setting non-deleted item to add to context.
            var itemType = _context.ItemTypes.FirstOrDefault();
            var item = new Item
            {
                Description = "test",
                IsDeleted = false,
                ItemPhoto = "-",
                ItemType = itemType,
                Name = "Blah",
                Price = 19.7m
            };
            _context.Items.Add(item);
            //Setting deleted item to add to context.
            var deletedItem = new Item
            {
                Description = "tes",
                IsDeleted = true,
                ItemPhoto = "-",
                ItemType = itemType,
                Name = "Bla",
                Price = 19.7m
            };
            _context.Items.Add(deletedItem);
            _context.SaveChanges();

            //Act
            var result = _controller.Index();

            //Assertion
            (result.ViewData.Model as IEnumerable<Item>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void UpdateItem_WhenCalled_ShouldUpdateGivenItem()
        {
            //Arrange
            var user = _context.Users.SingleOrDefault(u => u.UserName == "MarkT");
            _controller.MockCurrentUser(user.Id, user.UserName);

            var itemType = _context.ItemTypes.SingleOrDefault(i => i.Id == 1);
            var item = new Item
            {
                Description = "test",
                IsDeleted = false,
                ItemPhoto = "-",
                ItemType = itemType,
                Name = "Blah",
                Price = 19.7m
            };
            _context.Items.Add(item);
            _context.SaveChanges();

            //Act
            var result = _controller.Update(new ItemViewModel
            {
                Id = item.Id,
                Description = "change",
                Heading = "-",
                Name = "new",
                Price = 10.0m,
                ItemType = 2
            });

            //Assertion
            _context.Entry(item).Reload();
            item.Description.Should().Be("change");
            item.Name.Should().Be("new");
            item.Price.Should().Be(10.0m);
            item.ItemTypeId.Should().Be(2);
        }
    }
}
