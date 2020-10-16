namespace DBC.Infrastructure.DataAccess
{
    public class Pagination
    {
        public Pagination()
        {
            PageNumber = 1;
            PageSize = int.MaxValue;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
