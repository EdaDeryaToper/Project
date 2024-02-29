using CartService.Abstractions;
using CartService.Exceptions;
using CartService.Models;
using Newtonsoft.Json;
using System.IO;

namespace CartService.Service
{
    public class DataService : IDataService
    {
        private readonly IConfiguration _configuration;

        public DataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CartProductModel GetProduct(string id)
        {
            var filePath = _configuration.GetValue<string>("Data:ProductFilePath");
            var json = File.ReadAllText(filePath);
            var products = JsonConvert.DeserializeObject<List<CartProductModel>>(json);
            var product = products.FirstOrDefault(x => x.productId == id);
            if(product == null )
            {
                throw new ProductNotFoundException(id);
            }
            return product;
        }

        
        public List<CartProductModel> GetProducts()
        {
            var filePath = _configuration.GetValue<string>("Data:ProductFilePath");
            var json = File.ReadAllText(filePath);
            var products = JsonConvert.DeserializeObject<List<CartProductModel>>(json);
            if (products == null || !products.Any())
            {
                throw new ProductNotFoundException("");
            }
            return products;
        }
    }
}
