

using StoreApi.src.application;
using StoreApi.src.application.DTOs;
using StoreApi.src.domain;
using StoreApi.src.infraestructure;

namespace StoreApi.src.api
{
    public static class ProductsApi
    {
        public static void MapProductEndPoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/products", async (CreateProductDTO createProductDto, AddProductUseCase addProductUseCase) =>
            {
                try
                {
                    var product = new Product
                    {
                        Name = createProductDto.Name,
                        Price = createProductDto.Price
                    };
                    await addProductUseCase.ExecuteAsync(product, createProductDto.CategoryIds);
                    return Results.Ok(product);
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { message = ex.Message });
                }
            });

            routes.MapGet("/api/products", async (int? userId, ProductRepository repository) =>
            {
                var products = await repository.GetAllProductsAsync(userId);
                return Results.Ok(products);
            });

            routes.MapGet("/api/products/{id:int}", async (int id, ProductRepository repository) =>
            {
                var category = await repository.GetProductByIdAsync(id);
                return category is not null ? Results.Ok(category) : Results.NotFound();
            });

            routes.MapPatch("/api/products/{id:int}", async (int id, UpdateProductDTO updateProductDTO, ProductRepository repository, UpdateProductUseCase updateProductUse) =>
            {
                var existingProduct = await repository.GetProductByIdAsync(id);
                if (existingProduct is null) return Results.NotFound();

                var response = await updateProductUse.ExecuteAsync(updateProductDTO, existingProduct);
                return Results.Ok(response);
            });

            routes.MapDelete("/api/products/{id:int}", async (int id, ProductRepository repository) =>
            {
                await repository.DeleteProductAsync(id);
                return Results.NoContent();
            });

            routes.MapGet("/api/products/filter", async (string? name, string? categoryIds, FilterProductsUseCase filterProductsUseCase) =>
            {
                var filterDTO = new FilterProductsDTO
                {
                    PathName = name,
                    CategoriesIds = categoryIds != null ? categoryIds.Split(',').Select(int.Parse).ToList() : [],
                };
                var response = await filterProductsUseCase.ExecuteAsync(filterDTO);
                return Results.Ok(response);
            });
        }
    }
}