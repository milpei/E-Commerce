namespace E_Commerce.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Type { get; set; } // "Customer" or "Admin"
        public ICollection<Order> Orders { get; set; } // Navigation property for related orders
    }
}
