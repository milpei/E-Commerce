using E_Commerce.Entities;
using E_Commerce.Models;
using E_Commerce.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace E_Commerce.Services
{
    public class ProductService(
        GenericRepository<Category> _categoryRepository,
        GenericRepository<Product> _productRepository,
        IWebHostEnvironment _webHostEnvironment
        )
    {

        public async Task<IEnumerable<ProductVM>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync(
                includes: new Expression<Func<Product, object>>[] { x => x.Category! }
                );


            var productsVM = products.Select(item =>
            new ProductVM
            {
                ProductId = item.ProductId,
                Category = new CategoryVM
                {
                    CategoryId = item.Category!.CategoryId,
                    Name = item.Category!.Name,
                },
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Stock = item.Stock,
                ImageName = item.ImageName,

            }).ToList();

            return productsVM;
        }

        public async Task<ProductVM> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var categories = await _categoryRepository.GetAllAsync();

            var productVM = new ProductVM();
            if (product != null)
            {
                productVM = new ProductVM
                {
                    ProductId = product.ProductId,
                    Category = new CategoryVM
                    {
                        CategoryId = product.Category.CategoryId,
                        Name = product.Category.Name,
                    },
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    ImageName = product.ImageName,
                };
            }

            productVM.Categories = categories.Select(item => new SelectListItem
            {
                Value = item.CategoryId.ToString(),
                Text = item.Name,
            }).ToList();

            return productVM;

        }

        public async Task AddAsync(ProductVM viewModel) 
        {
            if (viewModel.ImageFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString()+Path.GetExtension(viewModel.ImageFile.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using(var fileStream = new FileStream(filePath,FileMode.Create))
                    await viewModel.ImageFile.CopyToAsync(fileStream);

                viewModel.ImageName = uniqueFileName;

            }

            var entity = new Product
            {
                CategoryId = viewModel.Category.CategoryId,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Stock = viewModel.Stock,
                ImageName = viewModel.ImageName,
            };

            await _productRepository.AddAsync(entity);
        }
        public async Task EditAsync(ProductVM viewModel) 
        {
            var product = await _productRepository.GetByIdAsync(viewModel.ProductId);

            if (viewModel.ImageFile != null) 
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.ImageFile.FileName);
                string filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                    await viewModel.ImageFile.CopyToAsync(fileStream);

                if (!product.ImageName.IsNullOrEmpty())
                {
                    var previousImage = product.ImageName;
                    string deleteFilePath = Path.Combine(uploadFolder, previousImage);
                    
                    if(File.Exists(deleteFilePath)) File.Delete(deleteFilePath);
                }

                viewModel.ImageName = uniqueFileName;
            }
            else
            {
                viewModel.ImageName = product.ImageName;
            }

            product.CategoryId = viewModel.Category.CategoryId;
            product.Name = viewModel.Name;
            product.Description = viewModel.Description;
            product.Price = viewModel.Price;
            product.Stock = viewModel.Stock;
            product.ImageName = viewModel.ImageName;

            await _productRepository.EditAsync(product);
        }
        public async Task DeleteAsync(int id) 
        {
            var product = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(product!);

        }

    }
}
