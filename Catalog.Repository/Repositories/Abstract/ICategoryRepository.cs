using Catalog.Entity.Models;
using Catalog.Entity.Pagination;

namespace Catalog.Repository.Repositories.Abstract
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(PaginationParams requestParams, bool trackCanges);
        Task<Category> GetOneCategoryByIdWithProductAsync(int categoryId, PaginationParams requestParams);
        Task<int> GetCountAsync(int categoryId);

    }
}
