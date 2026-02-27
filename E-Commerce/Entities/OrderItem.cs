namespace E_Commerce.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order? Order { get; set; } // Navigation property for related order
        public Product? Product { get; set; } // Navigation property for related product
    }
}
