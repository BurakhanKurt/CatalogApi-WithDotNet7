
using AutoMapper;
using Catalog.Entity.DTOs;
using Catalog.Entity.Exceptions;
using Catalog.Entity.Extensions;
using Catalog.Entity.Models;
using Catalog.Entity.Pagination;
using Catalog.Entity.RequestFeatureas;
using Catalog.Repository.Repositories.Abstract;
using Catalog.Repository.UnitOfWorks.Abstract;
using Catalog.Service.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Catalog.Service.Decorator
{
    public class CategoryServiceV2 : ICategoryService
    {

        private const string CacheProductKey = "CategoryCache";
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _categoryMemoryCache;
        private readonly IMemoryCache _categoryWithProductMemoryCache;

        public CategoryServiceV2(ICategoryRepository repository,
            IMapper mapper, 
            IUnitOfWork unitOfWork, 
            IMemoryCache categoryMemoryCache, 
            IMemoryCache categoryWithProductMemoryCache)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryMemoryCache = categoryMemoryCache;
            _categoryWithProductMemoryCache = categoryWithProductMemoryCache;

            if (!_categoryMemoryCache.TryGetValue(CacheProductKey, out _))
            {
                _categoryMemoryCache.Set(CacheProductKey, _repository
                    .FindAll(false)
                    .ToList());
            }
        }

        public async Task<CategoryDto> CreateOneCategoryAsync(CategoryCreateDto createdCategory)
        {
            var category = _mapper.Map<Category>(createdCategory);

            await _repository.CreateAsync(category);

            await _unitOfWork.SaveAsync();

            await CacheAllProducts();

            var categoryResponse = _mapper.Map<CategoryDto>(category);

            return categoryResponse;
        }
       
        public async Task RemoveOneCategoryAsync(int categoryId)
        {
            var deletedCategory = await GetOneCategoryByIdCheckExistAsync(categoryId);

            _repository.Delete(deletedCategory);

            await CacheAllProducts();

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateOneCategoryAsync(CategoryDto updatedCategory)
        {
            var category = await GetOneCategoryByIdCheckExistAsync(updatedCategory.Id);

            category = _mapper.Map(updatedCategory, category);

            _repository.Update(category);

            await CacheAllProducts();

            await _unitOfWork.SaveAsync();
        }

        public Task<IEnumerable<CategoryDto>> GetAllCategoryAsync(
            PaginationParams requestParams, bool trackChanges)
        {
            //Get memory
            var product = _categoryMemoryCache.Get<IEnumerable<Category>>(CacheProductKey)
                .ApplyPaginationCollection(
                    requestParams.PageSize,
                    requestParams.PageNumber);

            var productDto = _mapper.Map<IEnumerable<CategoryDto>>(product);

            return Task.FromResult(productDto);
        }

        public Task<CategoryDto> GetOneCategoryByIdAsync(int cetagoryId)
        {
            var category = GetOneCategoryByIdCheckExistAsync(cetagoryId);

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Task.FromResult(categoryDto);
        }
        
        public async Task<HeaderData> GetHeaderDataAsync(int categoryId,
            PaginationParams requestParams)
        {
            var count = await _repository.GetCountAsync(categoryId);
            var headerData = CreateHeaderData(count, requestParams);

            return headerData;

        }
        
        public Task<HeaderData> GetHeaderDataAsync(PaginationParams requestParams)
        {
            var count = _categoryMemoryCache.Get<IEnumerable<Category>>(CacheProductKey).Count();
            var headerData = new HeaderData()
            {
                TotalCount = count,
                PageSize = requestParams.PageSize,
                CurrentPage = requestParams.PageNumber,
                TotalPage = (int)Math.Ceiling(count / (double)requestParams.PageSize)
            };

            return Task.FromResult(headerData);
        }

        public async Task<CategoryWithProductsDto> GetOneCategoryByIdWithProductAsync(
            int categoryId, PaginationParams requestParams)
        {
            var exist = await _repository.AnyAsync(c=> c.Id == categoryId);
            
            if ( ! exist)
                throw new CategoryNotFoundException(categoryId);

            var categoryWithProduct = await _repository
                .GetOneCategoryByIdWithProductAsync(
                categoryId,
                requestParams
                );

            var responseCategory = _mapper.Map<CategoryWithProductsDto>(categoryWithProduct);
            return responseCategory;
        }

        //-----PRIVATE-----//
        private Task<Category> GetOneCategoryByIdCheckExistAsync(int id)
        {
            var categories = _categoryMemoryCache.Get<List<Category>>(CacheProductKey);
            var category = categories.FirstOrDefault(x => x.Id == id);

            if (category is null)
                throw new CategoryNotFoundException(id);

            return Task.FromResult(category);
        }
        
        private async Task CacheAllProducts() =>
            _categoryMemoryCache.Set(CacheProductKey, await _repository.FindAll(false).ToListAsync());
        
        private HeaderData CreateHeaderData(int count, PaginationParams requestParams)
        {
            var headerData = new HeaderData()
            {
                TotalCount = count,
                PageSize = requestParams.PageSize,
                CurrentPage = requestParams.PageNumber,
                TotalPage = (int)Math.Ceiling(count / (double)requestParams.PageSize)
            };
            return headerData;
        }

    }
}
