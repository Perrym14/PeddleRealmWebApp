using PeddleRealmWebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PeddleRealmWebApp.ViewModels
{
    public class ItemViewModel
    {

        public string Name { get; set; }


        public decimal Price { get; set; }


        public byte ItemType { get; set; }

        public IEnumerable<ItemType> ItemTypes { get; set; }


        public string Description { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ItemPhoto { get; set; }

        public string Heading { get; set; }

    }
}