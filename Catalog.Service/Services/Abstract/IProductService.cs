
using Catalog.Entity.DTOs;
using Catalog.Entity.Models;
using Catalog.Entity.Pagination;
using Catalog.Entity.RequestFeatureas;
using System.Linq.Expressions;

namespace Catalog.Service.Services.Abstract
{
    public interface IProductService 
    {
        Task<IEnumerable<ProductDto>> GetAllProductAsync(PaginationParams requestParams, bool trackChanges);
        Task<ProductDto> GetOneProductByIdAsync(int productId);
        Task<ProductDto> CreateOneProductAsync(ProductCreateDto createdProduct);
        Task UpdateOneProductAsync(ProductDto updatedProduct);
        Task RemoveOneProductAsync(int productId);
        Task<HeaderData> GetHeaderDataAsync(PaginationParams requestParams);
    }
}
