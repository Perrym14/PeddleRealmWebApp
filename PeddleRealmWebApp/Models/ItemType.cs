using System.ComponentModel.DataAnnotations;

namespace PeddleRealmWebApp.Models
{
    public class ItemType
    {
        public byte Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}