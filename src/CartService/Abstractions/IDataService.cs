using CartService.Models;

namespace CartService.Abstractions
{
    public interface IDataService
    {
        List<CartProductModel> GetProducts();
        CartProductModel GetProduct(string id);
    }
}
