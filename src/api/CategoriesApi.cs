using StoreApi.src.application;
using StoreApi.src.application.DTOs;
using StoreApi.src.domain;
using StoreApi.src.infraestructure;

namespace StoreApi.src.api
{
    public static class CategoriesApi
    {
        public static void MapCategoryEndPoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/categories", async (Category category, AddCategoryUseCase addCategoryUseCase) =>
            {
                try
                {
                    await addCategoryUseCase.ExecuteAsync(category);
                    return Results.Ok(category);
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { message = ex.Message }); }

            });

            routes.MapGet("/api/categories", async (CategoryRepository repository) =>
            {
                var categories = await repository.GetAllCategoriesAsync();
                return Results.Ok(categories);
            });

            routes.MapGet("/api/categories/{id:int}", async (int id, CategoryRepository repository) =>
            {
                var category = await repository.GetCategoryByIdAsync(id);
                return category is not null ? Results.Ok(category) : Results.NotFound();
            });

            routes.MapPatch("/api/categories/{id:int}", async (int id, UpdateCategoryDTO updateCategoryDTO, CategoryRepository repository, UpdateCategoryUseCase updateCategoryUseCase) =>
            {
                var existingCategory = await repository.GetCategoryByIdAsync(id);
                if (existingCategory is null) return Results.NotFound();
                var response = await updateCategoryUseCase.ExecuteAsync(updateCategoryDTO, existingCategory);
                return Results.Ok(response);
            });

            routes.MapDelete("/api/categories/{id:int}", async (int id, CategoryRepository repository) =>
            {
                await repository.DeleteCategoryAsync(id);
                return Results.NoContent();
            });
        }
    }
}