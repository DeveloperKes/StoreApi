

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

            routes.MapDelete("/api/wish/", async (int userId, int productId, WishProductRepository repository, UserRepository userRepository, ProductRepository productRepository, DeleteWishProductUseCase deleteUseCase) =>
            {
                var user = await userRepository.GetUserByIdAsync(userId);
                var product = await productRepository.GetProductByIdAsync(productId);
                if (user != null && product != null)
                {
                    var response = await deleteUseCase.ExecuteAsync(product, user);
                    if (response) return Results.NoContent();
                }
                return Results.NotFound();
            });

            routes.MapGet("/api/wish", async (int? userId, WishProductRepository repository, UserRepository userRepository) =>
            {
                if (userId != null)
                {
                    var user = await userRepository.GetUserByIdAsync((int)userId);
                    if (user != null)
                    {
                        var response = await repository.GetAllWishProductByUserAsync(user);
                        Console.WriteLine(response.Count);
                        return Results.Ok(response);
                    }
                }
                return Results.NotFound();
            });
        }
    }
}