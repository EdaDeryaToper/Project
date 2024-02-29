using ExceptionHandler.Exceptions;

namespace ProductService.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(string productId)
            : base($"The product with {productId} doesn't exists.")
        {
        }
    }
}
