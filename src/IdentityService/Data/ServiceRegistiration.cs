using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
    public static class ServiceRegistiration
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")));

        }
        
    }
}
