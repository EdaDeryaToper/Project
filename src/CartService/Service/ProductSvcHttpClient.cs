using CartService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CartService.Service
{
    public class ProductSvcHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ProductSvcHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<List<CartProductModel>> GetItemsForSearchDb()
        {
            var getProduct= await _httpClient.GetFromJsonAsync<List<CartProductModel>>(_config["ProductServiceUrl"] + "/api/products/detail");
            

            return getProduct;
        }
       
    }
}
