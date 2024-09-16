

using Microsoft.EntityFrameworkCore;
using StoreApi.src.application.DTOs;
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

        public async Task<List<ProductDTO>> GetAllProductsAsync(int? userId)
        {

            var result = await _context.Products
                .GroupJoin(
                _context.WishProducts.Where(w => w.User.Id == userId),
                product => product.Id,
                wishProduct => wishProduct.Product.Id,
                (product, wishProducts) => new
                {
                    Product = product,
                    IsFavorite = wishProducts.Any()
                }
                ).Select(p => new ProductDTO
                {
                    Id = p.Product.Id,
                    Name = p.Product.Name,
                    IsFavorite = p.IsFavorite,
                    Description = p.Product.Description,
                    Price = p.Product.Price,
                    Image = p.Product.Image,
                    Categories = p.Product.Categories.ToList(),
                    Rate = p.Product.Rate

                })
                .ToListAsync();

            return result;
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
            if (!string.IsNullOrWhiteSpace(name)) query = query.Where(p => p.Name.Contains(name));
            if (categoryList.Count > 0)
            {
                query = query.Where(p => p.Categories.Any(c => categoryList.Contains(c)));
            }

            return await query.ToListAsync();
        }
    }
}