using Catalog.Entity.DTOs;
using Catalog.Entity.Models;
using Catalog.Entity.Pagination;
using Catalog.Entity.RequestFeatureas;
using System.Linq.Expressions;

namespace Catalog.Service.Services.Abstract
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
