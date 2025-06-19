using System.ComponentModel.DataAnnotations;

namespace CartMicroservice.DomainLayer.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        [Required]
        public string UserId { get; set; } = string.Empty;

        // Navigation Property (One Cart => Many Items)
        public List<CartItem> Items { get; set; } = new();
    }
}
