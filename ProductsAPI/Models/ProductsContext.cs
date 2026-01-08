
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models
{
    public class ProductsContext:IdentityDbContext<AppUser ,AppRole,int>
    {
       
        public ProductsContext(DbContextOptions<ProductsContext>options):base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 1, ProductName = "Iphone14", Price = 20000, Isactive = false });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 2, ProductName = "Iphone15", Price = 50000, Isactive = true });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 3, ProductName = "Iphone16", Price = 90000, Isactive = false });
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 4, ProductName = "Iphone17", Price = 120000, Isactive = true });
        }
        public DbSet<Product> Products { get; set; }
    }
}