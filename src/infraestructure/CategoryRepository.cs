
using Microsoft.EntityFrameworkCore;
using StoreApi.src.domain;
using StoreApi.src.infraestructure.data;

namespace StoreApi.src.infraestructure
{
    public class CategoryRepository(ApplicationDbContext context)
    {

        private readonly ApplicationDbContext _context = context;

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
            .ToListAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

    }
}