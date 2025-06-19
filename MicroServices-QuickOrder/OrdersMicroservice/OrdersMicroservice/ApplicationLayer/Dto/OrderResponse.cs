namespace ItemsMicroservice.ApplicationLayer.Dto
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
