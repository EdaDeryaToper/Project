using CartService.Models;
using Microsoft.EntityFrameworkCore;

namespace CartService.Data
{
    public class CartDbContext:DbContext
    {
        public CartDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Cart> Carts { get; set; }
    }
}
