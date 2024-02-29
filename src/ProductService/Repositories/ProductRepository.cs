using ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductService.Abstractions;
using ProductService.Data;
using ProductService.Entities;
using ProductService.Exceptions;

namespace ProductService.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly ProjectDbContext _context;
        private readonly ILoggerManager _logger;

        public ProductRepository(ProjectDbContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IEnumerable<Product>> GetAll(bool tracking = true)
        {
            var products = await _context.Products.ToListAsync();

            if (products == null || !products.Any())
            {
                throw new ProductNotFoundException("");
            }

            return products;
        }

        public async Task<Product> GetByIdAsync(string id, bool tracking = true)
        {
            var trackProduct= await _context.Products.Include(x => x.Category).ThenInclude(s => s.SubCategories).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            var noTrackProduct= await _context.Products.AsNoTracking().Include(x => x.Category).ThenInclude(s => s.SubCategories).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (trackProduct == null || noTrackProduct == null)
                throw new ProductNotFoundException(id);
            if (tracking)
                return trackProduct;
            else
                return noTrackProduct;
        }

       
    }
}
