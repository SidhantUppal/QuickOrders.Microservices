using System.ComponentModel.DataAnnotations;

namespace ItemsMicroservice.DomainLayer.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
