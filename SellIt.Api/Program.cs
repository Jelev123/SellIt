using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SellIt.Api.Contracts.Auth;
using SellIt.Api.Services.Auth;
using SellIt.Core.Contracts.Count;
using SellIt.Core.Contracts.Image;
using SellIt.Core.Contracts.Product;
using SellIt.Core.Contracts.User;
using SellIt.Core.Services.Count;
using SellIt.Core.Services.Image;
using SellIt.Core.Services.Product;
using SellIt.Core.Services.User;
using SellIt.Infrastructure.Data;
using SellIt.Infrastructure.Data.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<ICountService, CountService>();
builder.Services.AddTransient<IAuthenticationManager, AuthenticationManager>();

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(@"bin\Debug\net6.0\SellIt.Api.xml");
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
