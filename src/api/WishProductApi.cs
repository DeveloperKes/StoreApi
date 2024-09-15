

using StoreApi.src.application;
using StoreApi.src.application.DTOs;
using StoreApi.src.infraestructure;

namespace StoreApi.src.api
{
    public static class WishProductsApi
    {
        public static void MapWishProductEndPoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/wish", async (CreateWishProductDTO createWishProductDTO, UserRepository userRepository, ProductRepository productRepository, AddWishProductUseCase addWishProductUseCase) =>
            {
                try
                {
                    var user = await userRepository.GetUserByIdAsync(createWishProductDTO.UserId);
                    if (user == null) return Results.NotFound();
                    var product = await productRepository.GetProductByIdAsync(createWishProductDTO.ProductId);
                    if (product == null) return Results.NotFound();
                    var response = await addWishProductUseCase.ExecuteAsync(product, user);
                    return Results.Ok(response);
                }
                catch (ArgumentException ex) { return Results.BadRequest(new { message = ex.Message }); }
            });

            routes.MapDelete("/api/wish/{id:int}", async (int id, WishProductRepository repository) =>
            {
                await repository.DeleteWishProductAsync(id);
                Results.NoContent();
            });
        }
    }
}