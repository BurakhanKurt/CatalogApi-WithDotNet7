namespace Catalog.Entity.RequestFeatureas
{
    public class HeaderData
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviosPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPage;
    }
}
