using CartService.Abstractions;
using CartService.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CartService.Data;
using Microsoft.EntityFrameworkCore;
using ExceptionHandler;
using CartService.Exceptions;

namespace CartService.Repository
{
    public class CartRepository:ICartService
    {
        private readonly CartDbContext _context;
        private readonly ILoggerManager _logger;
        public CartRepository(CartDbContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<bool> AddToCartAsync(Cart model)
        {
            EntityEntry<Cart> entityEntry = await _context.Carts.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }


        public async Task<bool> RemoveFromCartAsync(string id)
        {
            Cart? cart = await _context.Carts.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            if(cart == null)
            {
                throw new CartNotFoundException(id);
            }
            return _context.Entry(cart).State == EntityState.Deleted;
        }
    }
}
