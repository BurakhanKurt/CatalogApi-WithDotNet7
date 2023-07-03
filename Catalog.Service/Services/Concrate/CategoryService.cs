using AutoMapper;
using Catalog.Entity.DTOs;
using Catalog.Entity.Exceptions;
using Catalog.Entity.Models;
using Catalog.Entity.Pagination;
using Catalog.Entity.RequestFeatureas;
using Catalog.Repository.Repositories.Abstract;
using Catalog.Repository.UnitOfWorks.Abstract;
using Catalog.Service.Services.Abstract;

namespace Catalog.Service.Services.Concrate
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(ICategoryRepository repositoryManager,
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _repository = repositoryManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<CategoryDto> GetOneCategoryByIdAsync(int categoryId)
        {
            var category = await GetOneCategoryByIdCheckExistAsync(categoryId);

            var categoryResponse = _mapper.Map<CategoryDto>(category);

            return categoryResponse;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync
            (PaginationParams requestParams, bool trackChanges)
        {
            var categories = await _repository
                .GetAllCategoriesAsync(requestParams,
                trackChanges
                );

            var categoriesResponse = _mapper.Map<List<CategoryDto>>(categories);

            return categoriesResponse;
        }

        public async Task<CategoryDto> CreateOneCategoryAsync
            (CategoryCreateDto createdCategory)
        {
            var category = _mapper.Map<Category>(createdCategory);

            await _repository.CreateAsync(category);

            await _unitOfWork.SaveAsync();

            var categoryResponse = _mapper.Map<CategoryDto>(category);

            return categoryResponse;
        }

        public async Task RemoveOneCategoryAsync(int categoryId)
        {
            var deletedCategory = await GetOneCategoryByIdCheckExistAsync(categoryId);

            _repository.Delete(deletedCategory);

            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateOneCategoryAsync(CategoryDto updatedCategory)
        {
            var category = await GetOneCategoryByIdCheckExistAsync(updatedCategory.Id);

            category = _mapper.Map(updatedCategory, category);

            _repository.Update(category);

            await _unitOfWork.SaveAsync();
        }

        public async Task<CategoryWithProductsDto> GetOneCategoryByIdWithProductAsync
            (int categoryId, PaginationParams requestParams)
        {
            var categoryWithProduct = await _repository
                .GetOneCategoryByIdWithProductAsync(
                categoryId,
                requestParams
                );
            var responseCategory = _mapper.Map<CategoryWithProductsDto>(categoryWithProduct);
            return responseCategory;
        }

        public async Task<HeaderData> GetHeaderDataAsync
            (int cetagoryId, PaginationParams requestParams)
        {
            var count = await _repository.GetCountAsync(cetagoryId);
            var headerData = CreateHeaderData(count, requestParams);

            return headerData;
        }

        public async Task<HeaderData> GetHeaderDataAsync(PaginationParams requestParams)
        {
            var count = await _repository.GetCountAsync();
            var headerData = CreateHeaderData(count, requestParams);

            return headerData;
        }

        private HeaderData CreateHeaderData(int count,PaginationParams requestParams)
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

        private async Task<Category> GetOneCategoryByIdCheckExistAsync(int id)
        {
            var category = await _repository
                .GetByIdAsync(id);

            if (category is null)
                throw new CategoryNotFoundException(id);

            return category;
        } 

    }
}
