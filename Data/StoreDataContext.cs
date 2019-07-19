
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Maps;
using ProductCatalog.Models;

namespace ProductCatalog.Data
{
    public class StoreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            // optionBuilder.UseSqlServer(@"Server= DESKTOP-H6UQ71S\SQLEXPRESS; Database= ProductCatalog; Integrated Security=True; User ID=sa; Password=165044");

            optionBuilder.UseSqlServer(@"Server= DESKTOP-H6UQ71S\SQLEXPRESS; Database= ProductCatalog; Integrated Security=True; User ID=nicolas; Password=nicolas165044");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
        }
    }
}