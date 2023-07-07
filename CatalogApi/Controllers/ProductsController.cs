using Catalog.Entity.ActionFilter;
using Catalog.Entity.DTOs;
using Catalog.Repository.Pagination;
using Catalog.Entity.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Api.ActionFilter;

namespace Catalog.Entity.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> OneProductCreate([FromBody] ProductCreateDto product)
        {
            var response = await _productService.CreateOneProductAsync(product);

            return StatusCode(201, response);
        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneProductById([FromRoute(Name = "id")] int productId)
        {
            var response = await _productService.GetOneProductByIdAsync(productId);

            return StatusCode(200, response);
        }

        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveOneProductById([FromRoute(Name = "id")] int productId)
        {
            await _productService.RemoveOneProductAsync(productId);

            return StatusCode(200);
        }

        
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut]
        public async Task<IActionResult> UpdateOneProduct([FromBody] ProductDto updatedProduct)
        {
            await _productService.UpdateOneProductAsync(updatedProduct);

            return StatusCode(200);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationParams requestParams)
        {
            var response = await _productService.GetAllProductAsync(requestParams, false);
            var headerData = await _productService.GetHeaderDataAsync(requestParams);

            Response.Headers.Add("Entity",
                JsonSerializer.Serialize(headerData));

            return StatusCode(200, response);
        }
    }
}
