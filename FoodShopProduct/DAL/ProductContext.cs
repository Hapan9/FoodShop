using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public sealed class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
            
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInfo> ProductInfos { get; set; }
        public DbSet<ProductScore> ProductScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductInfo>()
                .HasOne(pi => pi.Product)
                .WithOne(p => p.ProductInfo)
                .HasForeignKey<ProductInfo>(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductScore>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductScores)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}