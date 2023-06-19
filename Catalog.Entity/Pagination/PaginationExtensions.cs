
namespace Catalog.Entity.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> ApplyPaginationQueryable<T>(this IQueryable<T> query, int pageSize, int pageNumber)
        {
            var pagedQuery = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return pagedQuery;
        }
        public static IEnumerable<T> ApplyPaginationCollection<T>(this IEnumerable<T> collection, int pageSize, int pageNumber)
        {
            var pagedCollection = collection
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


            return pagedCollection;
        }

    }
}
