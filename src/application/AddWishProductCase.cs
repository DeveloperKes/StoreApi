using StoreApi.src.application.DTOs;
using StoreApi.src.domain;
using StoreApi.src.infraestructure;

namespace StoreApi.src.application
{
    public class AddWishProductUseCase(WishProductRepository wishProductRepository)
    {
        private readonly WishProductRepository _wishProductRepository = wishProductRepository;

        public async Task<WishProductResponseDTO?> ExecuteAsync(Product product, User user)
        {
            var wishOld = await _wishProductRepository.FindOneByAsync(user, product);
            if (wishOld == null)
            {
                var wishProduct = new WishProduct
                {
                    Product = product,
                    User = user,
                    DateAdd = DateTime.UtcNow
                };

                await _wishProductRepository.AddWishProduct(wishProduct);
                return new WishProductResponseDTO
                {
                    Product = product,
                    DateAdd = wishProduct.DateAdd,
                    Id = wishProduct.Id
                };
            }
            else
                return new WishProductResponseDTO
                {
                    Product = wishOld.Product,
                    Id = wishOld.Id,
                    DateAdd = wishOld.DateAdd
                };
        }
    }
}