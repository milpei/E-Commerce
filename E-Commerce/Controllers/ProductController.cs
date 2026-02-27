using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class ProductController(ProductService _productService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> AddEdit(int id)
        {
            var productVM = await _productService.GetByIdAsync(id);
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(ProductVM entityVM)
        {
            ViewBag.message = null;

            ModelState.Remove("Categories");
            ModelState.Remove("Category.Name");

            if (!ModelState.IsValid) return View(entityVM);


            if (entityVM.ProductId == 0)
            {
                await _productService.AddAsync(entityVM);
                ModelState.Clear();
                entityVM = new ProductVM();
                ViewBag.message = "Product created successfully";
            }
            else
            {
                await _productService.EditAsync(entityVM);
                ViewBag.message = "Edited category";
            }

            return View(entityVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
