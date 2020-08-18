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
using System;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

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

            

            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IShoppingCartItemService, ShoppingCartItemManager>();
            services.AddTransient<IShoppingCartItemRepository, ShoppingCartItemRepository>();



            services.AddSwaggerGen((options) =>
            {
                options.SwaggerGeneratorOptions.IgnoreObsoleteActions = true;
                options.SwaggerDoc(Configuration.GetSection("Swagger:SwaggerName").Value, new OpenApiInfo()
                {
                    Version = Configuration.GetSection("Swagger:SwaggerDoc:Version").Value,
                    Title = Configuration.GetSection("Swagger:SwaggerDoc:Title").Value,
                    Description = Configuration.GetSection("Swagger:SwaggerDoc:Description").Value,
                    TermsOfService = new Uri(Configuration.GetSection("Swagger:SwaggerDoc:TermsOfService").Value),
                    Contact = new OpenApiContact
                    {
                        Name = Configuration.GetSection("Swagger:SwaggerDoc:Contact:Name").Value,
                        Email = string.Empty,
                        Url = new Uri(Configuration.GetSection("Swagger:SwaggerDoc:Contact:Url").Value),
                    },
                    License = new OpenApiLicense
                    {
                        Name = Configuration.GetSection("Swagger:SwaggerDoc:License:Name").Value,
                        Url = new Uri(Configuration.GetSection("Swagger:SwaggerDoc:License:Url").Value),
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                    }
                });
            });

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44318",
                    ValidAudience = "https://localhost:44318",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ECommerceApiAuthentication"))
                };
            });
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

            app.UseCors(builder => 
                builder.AllowAnyOrigin() //TODO
            );

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: String.Format("/swagger/{0}/swagger.json", "JWTUygulamasiWebAPI-Swagger"), name: "Version CoreSwaggerWebAPI-1");
                //c.DocExpansion(DocExpansion.None);

            });
        }
    }
}
