using Microsoft.EntityFrameworkCore;
using StoreApi.src.domain;

namespace StoreApi.src.infraestructure.data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        // Declaración de entidades
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WishProduct> WishProducts { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryProduct",
                    j => j.HasOne<Category>().WithMany().HasForeignKey("CategoriesId"),
                    j => j.HasOne<Product>().WithMany().HasForeignKey("ProductsId"));

            modelBuilder.Entity<User>()
            .HasOne(u => u.Person)
            .WithOne(p => p.User)
            .HasForeignKey<Person>(p => p.UserId);
        }
    }

}