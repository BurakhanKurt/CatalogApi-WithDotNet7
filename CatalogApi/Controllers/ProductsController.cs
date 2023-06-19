
using Catalog.Entity.DTOs;
using Catalog.Entity.Pagination;
using Catalog.Service.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

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

            Response.Headers.Add("Pagination",
                JsonSerializer.Serialize(headerData));

            return StatusCode(200, response);
        }
    }
}
