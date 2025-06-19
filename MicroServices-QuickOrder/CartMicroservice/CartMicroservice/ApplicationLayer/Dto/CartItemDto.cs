﻿namespace CartMicroservice.ApplicationLayer.Dto
{
    public class CartItemDto
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
        public string ProductImage { get; set; } = string.Empty;
    }
}
