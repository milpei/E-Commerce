using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
