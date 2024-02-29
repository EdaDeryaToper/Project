using Microsoft.EntityFrameworkCore;
using ProductService.Abstractions;
using ProductService.Data;
using ProductService.Entities;
using ProductService.Exceptions;

namespace ProductService.Repositories
{
    public class SubCategoryRepository:ISubCategoryRepository
    {
        private readonly ProjectDbContext _context;

        public SubCategoryRepository(ProjectDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SubCategory>> GetAll(bool tracking = true)
        {
            var categories = await _context.SubCategories.ToListAsync();

            if (categories == null || !categories.Any())
            {
                throw new CategoryNotFoundException("");
            }

            return categories;
        }

        public async Task<SubCategory> GetByIdAsync(string id, bool tracking = true)
        {
            var trackCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            var noTrackCategory = await _context.SubCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if (trackCategory == null || noTrackCategory == null)
                throw new CategoryNotFoundException(id);
            if (tracking)
                return trackCategory;
            else
                return noTrackCategory;
        }

      
    }
}
