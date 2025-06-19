using System.ComponentModel.DataAnnotations;

namespace ItemsMicroservice.ApplicationLayer.Dto
{
    public class OrderRequest
    {
        public int UserId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
