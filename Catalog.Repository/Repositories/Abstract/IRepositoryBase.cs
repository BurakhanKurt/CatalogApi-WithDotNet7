using Catalog.Entity.Models;
using System.Linq.Expressions;

namespace Catalog.Repository.Repositories.Abstract
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        // Tüm T nesnelerini getirir
        IQueryable<T> FindAll(bool trackChanges);

        // Belirli bir koşulu karşılayan T nesnelerini getirir
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        
        //Id ile T nesnesi getirir
        Task<T> GetByIdAsync(int id);

        // Belirli bir koşula gore T nesnesinin varligini kontrol eder 
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        // T nesnesini veritabanına asenkron olarak ekler
        Task CreateAsync(T entity);

        // T nesnesini günceller
        void Update(T entity);

        // T nesnesini siler
        void Delete(T entity);

        // Count
        Task<int> GetCountAsync();
    }
}