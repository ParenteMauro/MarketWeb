using BackEnd.Data;
using BackEnd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<MarketDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDb")));
builder.Services.AddControllers();
builder.Services.AddScoped<ICreateStockService, CreateStockService>();
var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    MarketDBContext context = scope.ServiceProvider.GetRequiredService<MarketDBContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

