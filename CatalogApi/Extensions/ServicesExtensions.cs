using Api.ActionFilter;
using Catalog.Entity.ActionFilter;
using Catalog.Entity.Mapping;
using Catalog.Repository;
using Catalog.Repository.Repositories.Abstract;
using Catalog.Repository.Repositories.Concrate;
using Catalog.Repository.UnitOfWorks.Abstract;
using Catalog.Repository.UnitOfWorks.Concrate;
using Catalog.Entity.Decorator;
using Catalog.Entity.Logging.Abstract;
using Catalog.Entity.Logging.Concrate;
using Catalog.Entity.Services.Abstract;
using Catalog.Entity.Services.Concrate;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Entity.Extesions
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

        //Entity
        public static void ConfigureServices(this IServiceCollection services)
        {
            //No caching service
            //services.AddScoped<IProductService, ProductService>();
            

            //Decorator -> Caching Entity
            services.AddScoped<IProductService, ProductServiceV2>();

            services.AddScoped<ICategoryService, CategoryService>();

            //uow
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            //Logging
            services.AddSingleton<ILoggerService, LoggerService>();

        }

        //ActionFilter
        public static void ConfigureActionFilter(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<LogFilterAttribute>();
        }

        //AutoMapper
        public static void ConfigureMapProfile(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(MapProfile));

        //Caching
        public static void ConfigureMemoryCaching(this IServiceCollection services) => 
            services.AddMemoryCache();

        public static void ConfigureResponseCaching(this IServiceCollection services) => 
            services.AddResponseCaching();

        





    }
}
