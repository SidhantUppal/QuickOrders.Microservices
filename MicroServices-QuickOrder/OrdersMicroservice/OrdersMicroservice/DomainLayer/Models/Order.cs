using System.ComponentModel.DataAnnotations;

namespace ItemsMicroservice.DomainLayer.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
