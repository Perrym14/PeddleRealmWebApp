using System.ComponentModel.DataAnnotations;

namespace PeddleRealmWebApp.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ItemType ItemType { get; set; }

        [Required]
        public byte ItemTypeId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public string ItemPhoto { get; set; }
    }
}