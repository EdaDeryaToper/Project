using System.Text.Json.Serialization;

namespace CartService.Models
{
    public class CartProductModel
    {
        [JsonPropertyName("productId")]
        public string productId { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTime createdDate { get; set; }

        [JsonPropertyName("updatedDate")]
        public DateTime updatedDate { get; set; }

        [JsonPropertyName("price")]
        public int price { get; set; }

        [JsonPropertyName("productName")]
        public string productName { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("imageUrl")]
        public string imageUrl { get; set; }

        [JsonPropertyName("brand")]
        public string brand { get; set; }

        [JsonPropertyName("categoryId")]
        public string categoryId { get; set; }

        [JsonPropertyName("categoryName")]
        public string categoryName { get; set; }

        [JsonPropertyName("user")]
        public string user { get; set; }

        [JsonPropertyName("subCategories")]
        public object subCategories { get; set; }
    }
}
