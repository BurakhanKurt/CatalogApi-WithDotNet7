using Catalog.Entity.DTOs;
using Catalog.Repository.Pagination;
using Catalog.Entity.RequestFeatureas;

namespace Catalog.Entity.Services.Abstract
{
    public interface ICategoryService 
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoryAsync(PaginationParams requestParams,bool trackChanges);
        Task<CategoryDto> GetOneCategoryByIdAsync(int cetagoryId);
        Task<CategoryDto> CreateOneCategoryAsync(CategoryCreateDto createdCategory);
        Task UpdateOneCategoryAsync(CategoryDto updatedCategory);
        Task RemoveOneCategoryAsync(int cetegoryId);
        Task<CategoryWithProductsDto> GetOneCategoryByIdWithProductAsync(int categoryId, PaginationParams requestParams);
        Task<HeaderData> GetHeaderDataAsync(int cateogryId,PaginationParams requestParams);
        //override
        Task<HeaderData> GetHeaderDataAsync(PaginationParams requestParams);

    }
}
