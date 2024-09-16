
using StoreApi.src.domain;
using StoreApi.src.infraestructure;

namespace StoreApi.src.application
{
    public class DeleteWishProductUseCase(WishProductRepository wishProductRepository)
    {
        private readonly WishProductRepository _wishProductRepository = wishProductRepository;

        public async Task<Boolean> ExecuteAsync(Product product, User user)
        {
            var wish = await _wishProductRepository.FindOneByAsync(user, product);
            if (wish != null)
            {
                await _wishProductRepository.DeleteWishProductAsync(wish.Id);
                return true;
            }
            return false;
        }
    }
}