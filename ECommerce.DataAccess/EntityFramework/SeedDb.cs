using ECommerce.Entities.Domain.Catalog;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ECommerce.DataAccess.EntityFramework
{
    public class SeedDb
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            Category category = new Category
            {
                Name = "Category 1",
                Description = "Category Description 1",
                CreatedOnUtc = DateTime.Now,
                Deleted = false,
                DisplayOrder = 0,
                Published = true,
                UpdatedOnUtc = DateTime.Now
            };

            if (!context.Categories.Any())
            {
                context.Categories.Add(category);

                if (context.SaveChanges() > 0)
                {
                    Product product = new Product
                    {
                        Name = "Product 1",
                        Price = 4.95m,
                        Description = "Product Description 1",
                        CreatedOnUtc = DateTime.Now,
                        Deleted = false,
                        DisplayOrder = 1,
                        Gtin = "123",
                        MinStockQuantity = 1,
                        OldPrice = 10.0m,
                        Published = true,
                        Sku = "G1234",
                        StockQuantity = 5,
                        UpdatedOnUtc = DateTime.Now,
                        CategoryId = category.Id,
                        Category = category
                    };

                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
        }
    }
}
