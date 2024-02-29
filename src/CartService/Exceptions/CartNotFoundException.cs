using ExceptionHandler.Exceptions;

namespace CartService.Exceptions
{
    public sealed class CartNotFoundException: NotFoundException
    {
        public CartNotFoundException(string cartId)
            : base($"The cart with {cartId} doesn't exists.")
        {
        }
    }
}
