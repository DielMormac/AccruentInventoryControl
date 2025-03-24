using AccruentInventoryControl.Application.Mappers;
using AccruentInventoryControl.Application.Services;
using AccruentInventoryControl.Application.Services.Interfaces;
using AccruentInventoryControl.Infrastructure.Database.Abstract;
using AccruentInventoryControl.Infrastructure.Repository;
using AccruentInventoryControl.Infrastructure.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var config = builder.Configuration;

// Database
services.AddSingleton<IDatabaseContext, DatabaseContext>();
//AddSingleton

services.AddSingleton<IConfiguration>(_ => config);

//Repositories
services.AddTransient<IProductRepository, ProductRepository>();
services.AddTransient<IWarehouseTransactionRepository, WarehouseTransactionRepository>();

//Services
services.AddTransient<IProductService, ProductService>();
services.AddTransient<IWarehouseTransactionService, WarehouseTransactionService>();

//Report Services
services.AddTransient<IWarehouseTransactionReportService, WarehouseTransactionReportService>();

// AutoMapper
services.AddAutoMapper(mc =>
{
    mc.AddProfile(new ProductMapper());
    mc.AddProfile(new WarehouseTransactionMapper());
    mc.AddProfile(new WarehouseTransactionReportMapper());
});

//InMemory DB
services.AddSingleton<IInMemoryDataBaseInitializerService, InMemoryDataBaseInitializerService>();

services.AddControllers();

// Configure CORS
services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable CORS
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();