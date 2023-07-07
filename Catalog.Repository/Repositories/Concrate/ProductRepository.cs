using Catalog.Entity.Models;
using Catalog.Repository.Repositories.Abstract;
using Catalog.Repository.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repository.Repositories.Concrate
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Product>> GetAllProductsAsync
            (PaginationParams requestParams, bool trackCanges)
        {
            return
                await FindAll(trackCanges)
                .ApplyPaginationQueryable(
                    requestParams.PageSize,
                    requestParams.PageNumber)
                .ToListAsync();

        }

    }
}
