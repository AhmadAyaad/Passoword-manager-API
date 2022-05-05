namespace PasswordManager.Common.Cross_cutting
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            PageNumber = 0;
            PageSize = 50;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 0 ? 0 : pageNumber;
            PageSize = pageSize > 50 ? 50 : pageSize;
        }
    }
}
