using Catalog.Entity.Models;
using Catalog.Entity.Pagination;

namespace Catalog.Repository.Repositories.Abstract
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetAllProductsAsync
            (PaginationParams requestParams, bool trackCanges);
    }
}
