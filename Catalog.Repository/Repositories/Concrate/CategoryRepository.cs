﻿using Catalog.Repository.Pagination;
using Catalog.Entity.Models;
using Catalog.Repository.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repository.Repositories.Concrate
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync
            (PaginationParams requestParams, bool trackCanges)
        {
            return
               await FindAll(trackCanges)
                .ApplyPaginationQueryable(
                    requestParams.PageSize,
                    requestParams.PageNumber).ToListAsync();
        }

        public async Task<Category> GetOneCategoryByIdWithProductAsync
            (int categoryId, PaginationParams requestParams)
        {
            var category = await FindByCondition(x => x.Id == categoryId,false)
                .Select(c => new Category 
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = c.Products
                        .ApplyPaginationCollection(
                        requestParams.PageSize,
                        requestParams.PageNumber)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task<int> GetCountAsync(int categoryId)
        {
            var count = await _context.Products
                .Where(c => c.CategoryId == categoryId).CountAsync();

            return count;
        }
    }
}
