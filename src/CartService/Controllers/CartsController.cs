using CartService.Abstractions;
using CartService.Models;
using ExceptionHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ICartService _cartService;
        private readonly ILoggerManager _logger;

        public CartsController(IDataService dataService, ICartService cartService,ILoggerManager logger)
        {
            _dataService = dataService;
            _cartService = cartService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _dataService.GetProducts();

            if (products == null || products.Count == 0)
            {
                
            }

            return Ok(products);
        }
        
        [HttpPost("addtocart")]
        public IActionResult AddToCart([FromBody] string productId)
        {
            var product = _dataService.GetProduct(productId);
            if (product != null)
            {
                var cart = new Cart
                {
                    Id = Guid.Parse(productId),
                    Brand = product.brand,
                    CategoryName = product.categoryName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    ProductName = product.productName,
                    CartName = "sepet",
                    UserName = User.Identity.Name

                };
                if (string.IsNullOrEmpty(cart.UserName))
                    cart.UserName = "Anonim";
                _cartService.AddToCartAsync(cart);

                return Ok(new { Message = "Product added to cart successfully" });
            }

            return BadRequest();

        }
        
        [HttpPost("removefromcart")]
        public IActionResult RemoveFromCart([FromBody] CartProductModel cartItem)
        {
            if (cartItem.user == User.Identity.Name)
            {
                _cartService.RemoveFromCartAsync(cartItem.productId);
                return Ok(new { Message = "Product removed from cart successfully" });
            }
            return BadRequest();
            
        }
    }
}
