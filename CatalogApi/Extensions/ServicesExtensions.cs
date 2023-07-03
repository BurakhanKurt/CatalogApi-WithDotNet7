﻿using Catalog.Api.Mapping;
using Catalog.Repository;
using Catalog.Repository.Repositories.Abstract;
using Catalog.Repository.Repositories.Concrate;
using Catalog.Repository.UnitOfWorks.Abstract;
using Catalog.Repository.UnitOfWorks.Concrate;
using Catalog.Service.Decorator;
using Catalog.Service.Services.Abstract;
using Catalog.Service.Services.Concrate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Extensions
{
    public static class ContextExtension
    {
        //Sql Context
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) => services
            .AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection"))
        );

        //Repository 
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        //Service
        public static void ConfigureServices(this IServiceCollection services)
        {
            //No caching service
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<ICategoryService, CategoryService>();

            //Decorator -> Caching Service
            services.AddScoped<IProductService, ProductServiceWithCaching>();
            services.AddScoped<ICategoryService, CategoryServiceWithCaching>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //AutoMapper
            services.AddAutoMapper(typeof(MapProfile));

        }

        public static void ConfigureMemoryCaching(this IServiceCollection services) => 
            services.AddMemoryCache();


    }
}
