using StoreApi.src.infraestructure.data;
using Microsoft.EntityFrameworkCore;
using StoreApi.src.infraestructure;
using StoreApi.src.application;
using StoreApi.src.api;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:8100") // Agrega las URL del frontend permitido
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<AddCategoryUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<AddProductUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<FilterProductsUseCase>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<AddUserUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<WishProductRepository>();
builder.Services.AddScoped<AddWishProductUseCase>();
builder.Services.AddScoped<DeleteWishProductUseCase>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCategoryEndPoints();
app.MapProductEndPoints();
app.MapUserEndPoints();
app.MapWishProductEndPoints();

app.UseCors("AllowSpecificOrigin");

app.Run();
