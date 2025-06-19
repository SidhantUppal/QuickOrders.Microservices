using System.ComponentModel.DataAnnotations;

namespace ItemsMicroservice.ApplicationLayer.Dto
{
    public class ItemRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
