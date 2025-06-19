namespace ItemsMicroservice.ApplicationLayer.Dto
{
    public class ItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
    }
}
