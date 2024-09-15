

using Microsoft.EntityFrameworkCore;
using StoreApi.src.domain;
using StoreApi.src.infraestructure.data;

namespace StoreApi.src.infraestructure
{
    public class ProductRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
            .Include(p => p.Categories)
            .ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> FindBy(List<Category> categoryList, string? name)
        {

            IQueryable<Product> query = _context.Products.Include(p => p.Categories);
            Console.WriteLine($"Este es el name: {name}");
            if (!string.IsNullOrWhiteSpace(name)) query = query.Where(p => p.Name.Contains(name));
            if (categoryList.Count > 0)
            {
                query = query.Where(p => p.Categories.Any(c => categoryList.Contains(c)));
            }

            return await query.ToListAsync();
        }
    }
}