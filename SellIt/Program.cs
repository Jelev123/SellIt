using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SellIt.Core.Contracts.Category;
using SellIt.Core.Contracts.Cloudinary;
using SellIt.Core.Contracts.Count;
using SellIt.Core.Contracts.ForAprooved;
using SellIt.Core.Contracts.Product;
using SellIt.Core.Services.Product;
using SellIt.Core.Services.Category;
using SellIt.Core.Services.Cloudinary;
using SellIt.Core.Services.Count;
using SellIt.Core.Services.ForAprooved;
using SellIt.Infrastructure.Data;
using SellIt.Infrastructure.Data.Models;
using System.Globalization;
using SellIt.Core.Contracts.Search;
using SellIt.Core.Services.Search;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICloduinaryService, CloudinaryService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IForAproovedService, ForAproovedService>();
builder.Services.AddTransient<ICountService, CountService>();
builder.Services.AddTransient<ISearchService, SearchService>();

Account cloudinaryCredentials = new Account(
                builder.Configuration["Cloudinary:CloudName"],
                builder.Configuration["Cloudinary:ApiKey"],
                builder.Configuration["Cloudinary:ApiSecret"]);

Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
builder.Services.AddSingleton(cloudinaryUtility);

var app = builder.Build();


CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
           name: "Area",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
