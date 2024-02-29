using ExceptionHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Abstractions;
using ProductService.Data;
using ProductService.Entities;
using ProductService.ViewModel;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILoggerManager _logger;

        public SearchController(IProductRepository productRepository, ILoggerManager logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> SearchItems(string searchTerm)
        {
            var query = await _productRepository.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            var searchResults = query.ToList();

            return Ok(searchResults);
        }

        [HttpGet("{SearchWithHelper}")]
        public async Task<ActionResult<List<ProductViewModel>>> SearchWithHelper([FromQuery] SearchParams searchParams)
        {
            var query = await _productRepository.GetAll();

            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchParams.SearchTerm));
            }

            query = searchParams.OrderBy switch
            {
                "categoryName" => query.OrderBy(x => x.Category.Name),
                "new" => query.OrderByDescending(x => x.CreatedDate),
                _ => query.OrderBy(x => x.Name)
            };

            if (!string.IsNullOrEmpty(searchParams.CategoryName))
            {
                query = query.Where(p => p.Category.Name.Contains(searchParams.CategoryName));
            }

            if (!string.IsNullOrEmpty(searchParams.BrandName))
            {
                query = query.Where(p => p.Brand.Contains(searchParams.BrandName));
            }

            var searchResults = query.ToList();

            return Ok(searchResults);
        }
        
    }
}
