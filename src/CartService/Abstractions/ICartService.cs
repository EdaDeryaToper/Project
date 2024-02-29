using CartService.Models;

namespace CartService.Abstractions
{
    public interface ICartService
    {

        Task<bool> AddToCartAsync(Cart model);
        Task<bool> RemoveFromCartAsync(string id);
    }
}
