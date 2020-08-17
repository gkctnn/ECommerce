using ECommerce.Entities.Customer;
using ECommerce.Entities.Domain.Catalog;
using ECommerce.Entities.Order;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.EntityFramework
{
    public class AppDbContext : IdentityDbContext<Customer>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Category>().HasData(
            //    new Category
            //    {
            //        Name = "Category 1",
            //        Description = "Category Description 1",
            //        CreatedOnUtc = DateTime.Now,
            //        Deleted = false,
            //        DisplayOrder = 0,
            //        Published = true,
            //        UpdatedOnUtc = DateTime.Now
            //    },
            //    new Category
            //    {
            //        Name = "Category 2",
            //        Description = "Category Description 2",
            //        CreatedOnUtc = DateTime.Now,
            //        Deleted = false,
            //        DisplayOrder = 1,
            //        Published = true,
            //        UpdatedOnUtc = DateTime.Now
            //    },
            //    new Category
            //    {
            //        Name = "Category 3",
            //        Description = "Category Description 3",
            //        CreatedOnUtc = DateTime.Now,
            //        Deleted = false,
            //        DisplayOrder = 2,
            //        Published = true,
            //        UpdatedOnUtc = DateTime.Now
            //    }
            //    );

            //modelBuilder.Entity<Product>().HasData(
            //    new Product
            //    {
            //        Name = "Product 1",
            //        Price = 4.95m,
            //        Description = "Product Description 1",
            //        CreatedOnUtc = DateTime.Now,
            //        Deleted = false,
            //        DisplayOrder = 1,
            //        Gtin = "123",
            //        MinStockQuantity = 1,
            //        OldPrice = 10.0m,
            //        Published = true,
            //        Sku = "G1234",
            //        StockQuantity = 5,
            //        UpdatedOnUtc = DateTime.Now,
            //        CategoryId = 1
            //    },
            //    new Product
            //    {
            //        Name = "Product 2",
            //        Price = 5.95m,
            //        Description = "Product Description 2",
            //        CreatedOnUtc = DateTime.Now,
            //        Deleted = false,
            //        DisplayOrder = 2,
            //        Gtin = "234",
            //        MinStockQuantity = 2,
            //        OldPrice = 10.0m,
            //        Published = true,
            //        Sku = "G2345",
            //        StockQuantity = 5,
            //        UpdatedOnUtc = DateTime.Now,
            //        CategoryId = 2
            //    },
            //    new Product
            //    {
            //        Name = "Product 3",
            //        Price = 6.95m,
            //        Description = "Product Description 3",
            //        CreatedOnUtc = DateTime.Now,
            //        Deleted = false,
            //        DisplayOrder = 3,
            //        Gtin = "345",
            //        MinStockQuantity = 1,
            //        OldPrice = 10.0m,
            //        Published = true,
            //        Sku = "G3456",
            //        StockQuantity = 10,
            //        UpdatedOnUtc = DateTime.Now,
            //        CategoryId = 3
            //    }
            //    );
        }
    }
}
