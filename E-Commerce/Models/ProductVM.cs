using E_Commerce.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public CategoryVM Category { get; set; }
        public List<SelectListItem> Categories { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? ImageName { get; set; } = null;

        public IFormFile? ImageFile { get; set; }
    }
}


