using CartService.Abstractions;
using CartService.Data;
using CartService.Repository;
using ExceptionHandler;
using Microsoft.EntityFrameworkCore;

namespace CartService.Extensions
{
    public static class ServiceRegistiration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CartDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

            services.AddScoped<ICartService, CartRepository>();



        }
        public static void ConfigureLoggerManager(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
