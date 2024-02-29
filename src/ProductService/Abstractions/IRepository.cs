using System.Linq.Expressions;
using ProductService.Entities;

namespace ProductService.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
        
    }
}
