using ECommerce.Entities.Customer;
using ECommerce.Entities.Domain.Catalog;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ECommerce.DataAccess.EntityFramework
{
    public class SeedDb
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();

            var userManager = serviceProvider.GetRequiredService<UserManager<Customer>>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string roleAdmin = "admin";
            string roleCustomer = "customer";

            context.Database.EnsureCreated();

            Customer customer = new Customer()
            {
                UserName = "test",
                Email = "test@test.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            Customer admin = new Customer()
            {
                UserName = "gokcenur",
                Email = "zenginalgokcenur@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (!context.Users.Any())
            {
                await userManager.CreateAsync(customer, "@Password111");
                await userManager.CreateAsync(admin, "@Password123");

                if (!context.Roles.Any())
                {
                    if (await roleManager.FindByNameAsync(roleAdmin) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleAdmin));
                    }

                    if (await roleManager.FindByNameAsync(roleCustomer) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleCustomer));
                    }

                    await userManager.AddToRoleAsync(customer, roleCustomer);
                    await userManager.AddToRoleAsync(admin, roleAdmin);
                }
            }

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
                        StockQuantity = 10,
                        UpdatedOnUtc = DateTime.Now,
                        CategoryId = category.Id,
                        Category = category
                    };

                    context.Products.Add(product);

                    Product product2 = new Product
                    {
                        Name = "Product 2",
                        Price = 5.95m,
                        Description = "Product Description 2",
                        CreatedOnUtc = DateTime.Now,
                        Deleted = false,
                        DisplayOrder = 2,
                        Gtin = "234",
                        MinStockQuantity = 1,
                        OldPrice = 10.0m,
                        Published = true,
                        Sku = "G2345",
                        StockQuantity = 10,
                        UpdatedOnUtc = DateTime.Now,
                        CategoryId = category.Id,
                        Category = category
                    };

                    context.Products.Add(product2);

                    context.SaveChanges();
                }
            }
        }
    }
}
