using ExceptionHandler.Exceptions;

namespace CartService.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(string productId)
            : base($"The product with {productId} doesn't exists.")
        {
        }
    }
}
