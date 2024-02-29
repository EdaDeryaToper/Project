using ExceptionHandler.Exceptions;

namespace ProductService.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(string categoryId)
            : base($"The category with {categoryId} doesn't exists.")
        {
        }
    }
    
}
