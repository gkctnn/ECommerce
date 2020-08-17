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
using Microsoft.IdentityModel.Tokens;
using ECommerce.Entities.Customer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

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

            services.AddIdentity<Customer, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://localhost:44318",
                        ValidateAudience = true,
                        ValidAudience = "https://localhost:44318",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ECommerceApiAuthentication"))
                    };
                });

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

            SeedDb.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider).Wait();

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
