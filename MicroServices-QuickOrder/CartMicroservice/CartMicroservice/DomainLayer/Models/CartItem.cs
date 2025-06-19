using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CartMicroservice.DomainLayer.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        // Foreign Key
        public int CartId { get; set; }

        // Navigation Property (Each Item belongs to one Cart)
        [ForeignKey("CartId")]
        public Cart Cart { get; set; } = null!;
    }
}
