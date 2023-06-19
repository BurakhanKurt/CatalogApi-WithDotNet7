using Catalog.Entity.Models;
using Catalog.Repository.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Catalog.Repository.Repositories.Concrate
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        protected RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        
        // Tüm T nesnelerini getirir
        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges ?
                _dbSet.AsTracking() :
                _dbSet.AsNoTracking();

        // Belirli bir koşulu karşılayan T nesnelerini getirir
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges ?
                _dbSet.Where(expression).AsTracking():
                _dbSet.Where(expression).AsNoTracking();

        //Id ile T nesnesi getirir
        public async Task<T> GetByIdAsync(int id) => 
            await _dbSet.FindAsync(id);

        // Belirli bir koşula gore T nesnesinin varligini kontrol eder 
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression) => 
            await _dbSet.AnyAsync(expression);

        // T nesnesini veritabanına asenkron olarak ekler
        public async Task CreateAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        // T nesnesini günceller
        public void Update(T entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        // T nesnesini siler
        public void Delete(T entity) => 
            _dbSet.Remove(entity);
        
        //count
        public async Task<int> GetCountAsync()
        {
            return await _dbSet.CountAsync();
        }


    }
}
