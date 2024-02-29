using ExceptionHandler;
using Microsoft.EntityFrameworkCore;
using ProductService.Abstractions;
using ProductService.Data;
using ProductService.Repositories;

namespace ProductService.Extensions
{
    public static class ServiceRegistiration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProjectDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            

        }
        public static void ConfigureLoggerManager(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
