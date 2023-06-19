
namespace Catalog.Repository.UnitOfWorks.Abstract
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
