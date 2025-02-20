using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using BackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Market API", Version = "v1" });
});

builder.Services.AddDbContext<MarketDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDb")));

builder.Services.AddControllers();
builder.Services.AddScoped<IStockRepository, StockRepository>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    MarketDBContext context = scope.ServiceProvider.GetRequiredService<MarketDBContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Market API v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
