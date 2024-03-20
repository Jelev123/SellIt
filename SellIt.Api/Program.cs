using SellIt.ConfigExtensions;
using SellIt.Core.Handlers.Error;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDatabase(builder.Configuration);
builder.Services.RegisterServices();

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(@"bin\Debug\net6.0\SellIt.Api.xml");
});
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
