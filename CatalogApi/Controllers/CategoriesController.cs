﻿using Api.ActionFilter;
using Catalog.Entity.DTOs;
using Catalog.Repository.Pagination;
using Catalog.Entity.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Catalog.Entity.ActionFilter;

namespace Catalog.Entity.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> OneCategoryCreate([FromBody] CategoryCreateDto category)
        {
            var response = await _categoryService.CreateOneCategoryAsync(category);
            
            return StatusCode(201, response);
        }

        
        [ResponseCache(CacheProfileName = "30second")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneCategoryById([FromRoute(Name = "id")] int categoryId)
        {
            var response = await _categoryService.GetOneCategoryByIdAsync(categoryId);

            return StatusCode(200, response);
        }

        
        [HttpGet("withproducts/{id:int}")]
        public async Task<IActionResult> GetOneCategoryByIdWithProductsAsync
            ([FromRoute(Name = "id")] int categoryId, [FromQuery] PaginationParams requestParams)
        {
            var response = await _categoryService
                .GetOneCategoryByIdWithProductAsync(
                categoryId,
                requestParams
                );

            var headerData = await _categoryService.GetHeaderDataAsync(categoryId,requestParams);

            Response.Headers.Add("Entity",
                JsonSerializer.Serialize(headerData));

            return StatusCode(200, response);
        }

        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveOneCategoryById([FromRoute(Name = "id")] int cetegoryId)
        {
            await _categoryService.RemoveOneCategoryAsync(cetegoryId);

            return StatusCode(200);
        }

        
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut]
        public async Task<IActionResult> UpdateOneCategory([FromBody] CategoryDto categoryUpdated)
        {
            await _categoryService.UpdateOneCategoryAsync(categoryUpdated);

            return StatusCode(200);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] PaginationParams requestParams)
        {
            var response = await _categoryService.GetAllCategoryAsync(requestParams, false);

            var headerData = await _categoryService.GetHeaderDataAsync(requestParams);

            Response.Headers.Add("Entity",
                JsonSerializer.Serialize(headerData));

            return StatusCode(200, response);
        }

    }
}
