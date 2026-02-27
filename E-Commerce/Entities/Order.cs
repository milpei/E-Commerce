namespace E_Commerce.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public User? User { get; set; } // Navigation property for related user
        public ICollection<OrderItem> OrderItems { get; set; } // Navigation property for related order items
    
    }
}
