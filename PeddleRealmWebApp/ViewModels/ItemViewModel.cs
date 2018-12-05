using PeddleRealmWebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PeddleRealmWebApp.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public byte ItemType { get; set; }

        public IEnumerable<ItemType> ItemTypes { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ItemPhoto { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get { return (Id != 0) ? "Update" : "Create"; }
        }
    }
}