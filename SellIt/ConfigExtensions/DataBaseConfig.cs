namespace SellIt.ConfigExtensions;

using Microsoft.EntityFrameworkCore;
using SellIt.Infrastructure.Data;
using System.Configuration;

public static class DataBaseConfig
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
