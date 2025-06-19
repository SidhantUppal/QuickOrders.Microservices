namespace CartMicroservice.ApplicationLayer.Dto
{
    public class CartDto
    {
        public string UserId { get; set; } = string.Empty;
        public List<CartItemDto> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(item => item.Total);
    }
}
