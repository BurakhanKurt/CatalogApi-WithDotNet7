namespace Catalog.Entity.Pagination
{
    public class PaginationParams
    {
        // Auto-implement property
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 20;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > _pageSize ? _pageSize : value; }
        }
    }
}
