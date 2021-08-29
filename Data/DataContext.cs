using Microsoft.EntityFrameworkCore;
using ShopApi.DDD.AspNetCore.Model;

namespace ShopApi.DDD.AspNetCore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().Property(p => p.Price).HasPrecision(18,2).HasColumnType("decimal");
        }
    }
}