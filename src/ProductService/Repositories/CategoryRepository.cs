using Microsoft.EntityFrameworkCore;
using ProductService.Abstractions;
using ProductService.Data;
using ProductService.Entities;
using ProductService.Exceptions;

namespace ProductService.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ProjectDbContext _context;

        public CategoryRepository(ProjectDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAll(bool tracking = true)
        {
            var categories = await _context.Categories.ToListAsync();

            if (categories == null || !categories.Any())
            {
                throw new CategoryNotFoundException("");
            }

            return categories;
        }

        public async Task<Category> GetByIdAsync(string id, bool tracking = true)
        {
            var trackCategory = await _context.Categories.Include(s => s.SubCategories).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            var noTrackCategory = await _context.Categories.AsNoTracking().Include(s => s.SubCategories).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (trackCategory == null || noTrackCategory == null)
                throw new CategoryNotFoundException(id);
            if (tracking)
                return trackCategory;
            else
                return noTrackCategory;
        }

       
    }
}
