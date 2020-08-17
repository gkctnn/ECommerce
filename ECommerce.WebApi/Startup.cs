using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.DataAccess.EntityFramework;
using ECommerce.Business.Domain.Catalog;
using ECommerce.Business.Abstract.Domain.Catalog;
using ECommerce.Business.Abstract.Order;
using ECommerce.Business.Order;
using ECommerce.DataAccess.Abstract.Domain.Catalog;
using ECommerce.DataAccess.EntityFramework.Domain.Catalog;
using ECommerce.DataAccess.EntityFramework.Order;
using ECommerce.DataAccess.Abstract.Order;

namespace ECommerce.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ECommerceConnection")
                    , m => m.MigrationsAssembly("ECommerce.WebApi")
                )
            );

            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IShoppingCartItemService, ShoppingCartItemManager>();
            services.AddTransient<IShoppingCartItemRepository, ShoppingCartItemRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            SeedDb.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
