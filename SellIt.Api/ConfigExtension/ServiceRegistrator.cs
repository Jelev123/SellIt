namespace SellIt.Api.ConfigExtension
{

    using Microsoft.AspNetCore.Identity;
    using SellIt.Api.Contracts.Auth;
    using SellIt.Api.Services.Auth;
    using SellIt.Core.Contracts.Category;
    using SellIt.Core.Contracts.Count;
    using SellIt.Core.Contracts.ForAprooved;
    using SellIt.Core.Contracts.Image;
    using SellIt.Core.Contracts.Messages;
    using SellIt.Core.Contracts.Product;
    using SellIt.Core.Contracts.Search;
    using SellIt.Core.Contracts.User;
    using SellIt.Core.Repository;
    using SellIt.Core.Services.Category;
    using SellIt.Core.Services.Count;
    using SellIt.Core.Services.ForAprooved;
    using SellIt.Core.Services.Image;
    using SellIt.Core.Services.Message;
    using SellIt.Core.Services.Product;
    using SellIt.Core.Services.Search;
    using SellIt.Core.Services.User;
    using SellIt.Infrastructure.Data;
    using SellIt.Infrastructure.Data.Models;

    public static class ServiceRegistrator
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IForAproovedService, ForAproovedService>();
            services.AddTransient<ICountService, CountService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IMessagesService, MessageService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IUserService, UserService>();

            services.AddScoped<IProductService, ProductDecoratorService>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<ProductService>();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddHttpClient();
        }
    }
}
