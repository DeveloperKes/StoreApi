using Microsoft.EntityFrameworkCore;
using StoreApi.src.domain;
using StoreApi.src.infraestructure.data;

namespace StoreApi.src.infraestructure
{
    public class WishProductRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddWishProduct(WishProduct wishProduct)
        {
            _context.WishProducts.Add(wishProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<WishProduct?> GetWishProductByIdAsync(int id)
        {
            return await _context.WishProducts
            .Include(u => u.User)
            .Include(p => p.Product)
            .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<WishProduct>> GetAllWishProductByUserAsync(User user)
        {
            return await _context.WishProducts
            .Include(u => u.User)
            .Include(p => p.Product)
            .Where(u => u.User.Id == user.Id)
            .ToListAsync();
        }
        public async Task<WishProduct?> FindOneByAsync(User user, Product product)
        {
            return await _context.WishProducts
            .Include(u => u.User)
            .Include(p => p.Product)
            .Where(u => u.User.Id == user.Id)
            .Where(p => p.Product.Id == product.Id)
            .FirstOrDefaultAsync();
        }

        public async Task DeleteWishProductAsync(int id)
        {
            var wp = await _context.WishProducts.FindAsync(id);
            if (wp != null)
            {
                _context.WishProducts.Remove(wp);
                await _context.SaveChangesAsync();
            }
        }
    }
}