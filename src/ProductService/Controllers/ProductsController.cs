using System.Collections.Immutable;
using ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Abstractions;
using ProductService.Entities;
using ProductService.ViewModel;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private ILoggerManager _logger;

        public ProductsController(IProductRepository productRepository,ILoggerManager logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
                
                IEnumerable<Product> products = await _productRepository.GetAll();
                return Ok(products);
            
        }
       
        [HttpGet("detail")]
        public async Task<IActionResult> GetAllProductsWithDetail()
        {
            IEnumerable<Product> products = await _productRepository.GetAll();
            List<ProductViewModel> productsViewModel = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var category= await _productRepository.GetByIdAsync(product.Id.ToString(), true);
                ProductViewModel response = new ProductViewModel()
                {
                    ProductId = product.Id.ToString(),
                    ProductName = product.Name,
                    CreatedDate = product.CreatedDate,
                    UpdatedDate = product.UpdatedDate,
                    Brand = product.Brand,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    CategoryId = category.Category.Id.ToString(),
                    CategoryName = category.Category.Name,
                    User = "eda@example.com"
                };
                productsViewModel.Add(response);
            }
            
            return Ok(productsViewModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            Product product = await _productRepository.GetByIdAsync(id, true);
           
            ProductViewModel response = new ProductViewModel()
            {
                ProductId = product.Id.ToString(),
                ProductName = product.Name,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate,
                Brand = product.Brand,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                CategoryId = product.Category.Id.ToString(),
                CategoryName = product.Category.Name,
                SubCategories = product.Category.SubCategories.Select(s=>new SubCategoryViewModel
                {
                    SubCategoryId = s.Id.ToString(),
                    SubCategoryName = s.Name

                }).ToList()
                
            };
            
            return Ok(response);

        }

    }
}
