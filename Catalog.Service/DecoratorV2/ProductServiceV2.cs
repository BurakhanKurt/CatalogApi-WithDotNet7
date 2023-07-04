using AutoMapper;
using Catalog.Entity.DTOs;
using Catalog.Entity.Exceptions;
using Catalog.Entity.Extensions;
using Catalog.Entity.Models;
using Catalog.Entity.Pagination;
using Catalog.Entity.RequestFeatureas;
using Catalog.Repository.Repositories.Abstract;
using Catalog.Repository.UnitOfWorks.Abstract;
using Catalog.Service.Logging.Abstract;
using Catalog.Service.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;


namespace Catalog.Service.Decorator
{
    public class ProductServiceV2 : IProductService
    {
        private const string CacheProductKey = "ProductCache";
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
        private readonly ILoggerService _loggerService;

        public ProductServiceV2(IProductRepository repository,
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            ILoggerService loggerService)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
            _loggerService = loggerService;

            if (!memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _repository.FindAll(false).ToList());
            }

        }
        //Create
        public async Task<ProductDto> CreateOneProductAsync(ProductCreateDto createdProduct)
        {
            //TODO categoryler de memoryden çekilebilir mi ?
            var exist = await _categoryRepository.AnyAsync(c => c.Id == createdProduct.CategoryId);

            if (!exist)
            {
                //log
                _loggerService.LogError($" -> {createdProduct.CategoryId} idsine sahip category bulunamadı");
                throw new CategoryNotFoundException(createdProduct.CategoryId);
            }
                

            var product = _mapper.Map<Product>(createdProduct);

            await _repository.CreateAsync(product);

            await _unitOfWork.SaveAsync();
            
            //log
            _loggerService.LogInfo($" -> {product.Id} idsine sahip product eklendi");

            //Caching
            await CacheAllProducts();

            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
        //Remove
        public async Task RemoveOneProductAsync(int productId)
        {
            var deletedProduct = await GetOneProductByIdCheckExistAsync(productId);

            _repository.Delete(deletedProduct);

            await _unitOfWork.SaveAsync();

            //Caching
            await CacheAllProducts();
        }
        //Update
        public async Task UpdateOneProductAsync(ProductDto updatedProduct)
        {
            var product = await GetOneProductByIdCheckExistAsync(updatedProduct.Id);

            product = _mapper.Map(updatedProduct, product);

            _repository.Update(product);

            await _unitOfWork.SaveAsync();

            //Caching
            await CacheAllProducts();
        }

        public Task<IEnumerable<ProductDto>> GetAllProductAsync(PaginationParams requestParams, bool trackChanges)
        {
            //Get memory
            var product = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey)
                .ApplyPaginationCollection(
                    requestParams.PageSize,
                    requestParams.PageNumber);

            var productDto = _mapper.Map<IEnumerable<ProductDto>>(product);

            return Task.FromResult(productDto);
        }

        public Task<ProductDto> GetOneProductByIdAsync(int productId)
        {
            var product = GetOneProductByIdCheckExistAsync(productId);

            var productDto = _mapper.Map<ProductDto>(product);

            return Task.FromResult(productDto);
        }

        public Task<HeaderData> GetHeaderDataAsync(PaginationParams requestParams)
        {
            var count = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey).Count();

            var headerData = new HeaderData()
            {
                TotalCount = count,
                PageSize = requestParams.PageSize,
                CurrentPage = requestParams.PageNumber,
                TotalPage = (int)Math.Ceiling(count / (double)requestParams.PageSize)
            };

            return Task.FromResult(headerData);
        }

        private Task<Product> GetOneProductByIdCheckExistAsync(int id)
        {
            var products = _memoryCache.Get<List<Product>>(CacheProductKey);
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product is null)
                throw new ProductNotFoundException(id);

            return Task.FromResult(product);

        }

        private async Task CacheAllProducts() =>
            _memoryCache.Set(CacheProductKey, await _repository.FindAll(false).ToListAsync());

    }
}
