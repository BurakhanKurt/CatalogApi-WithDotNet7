using AutoMapper;
using Catalog.Entity.DTOs;
using Catalog.Entity.Exceptions;
using Catalog.Entity.Models;
using Catalog.Repository.Pagination;
using Catalog.Entity.RequestFeatureas;
using Catalog.Repository.Repositories.Abstract;
using Catalog.Repository.UnitOfWorks.Abstract;
using Catalog.Entity.Services.Abstract;

namespace Catalog.Entity.Services.Concrate
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository
            )
        {
            _repository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductDto> GetOneProductByIdAsync(int id)
        {
            var product = await GetOneProductByIdCheckExistAsync(id);

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductAsync
            (PaginationParams requestParams, bool trackChanges)
        {
            var product = await _repository
                .GetAllProductsAsync(
                requestParams,
                trackChanges
                );

            var productDto = _mapper.Map<List<ProductDto>>(product);

            return productDto;
        }

        public async Task<ProductDto> CreateOneProductAsync
            (ProductCreateDto createdProduct)
        {
            var product = _mapper.Map<Product>(createdProduct);

            var exist = await _categoryRepository.AnyAsync(c=> c.Id == product.CategoryId);
            
            if (exist) 
                throw new CategoryNotFoundException(product.CategoryId);

            await _repository.CreateAsync(product);

            await _unitOfWork.SaveAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task RemoveOneProductAsync(int productId)
        {
            var deletedProduct = await GetOneProductByIdCheckExistAsync(productId);

            _repository.Delete(deletedProduct);

            await _unitOfWork.SaveAsync();
        }
        //TODO Category ıd kontrol et
        public async Task UpdateOneProductAsync(ProductDto updatedProduct)
        {
            var product = await GetOneProductByIdCheckExistAsync(updatedProduct.Id);

            product = _mapper.Map(updatedProduct, product);

            _repository.Update(product);

            await _unitOfWork.SaveAsync();
        }


        public async Task<HeaderData> GetHeaderDataAsync(PaginationParams requestParams)
        {
            var count = await _repository.GetCountAsync();
            
            var headerData = new HeaderData()
            {
                TotalCount = count,
                PageSize = requestParams.PageSize,
                CurrentPage = requestParams.PageNumber,
                TotalPage = (int)Math.Ceiling(count / (double)requestParams.PageSize)
            };

            return headerData;
        }

        private async Task<Product> GetOneProductByIdCheckExistAsync(int id)
        {
            var product = await _repository
                .GetByIdAsync(id);

            if (product is null)
                throw new ProductNotFoundException(id);

            return product;

        }

    }
}
