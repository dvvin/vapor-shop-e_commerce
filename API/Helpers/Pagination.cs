namespace API.Helpers
{
    public class Pagination<T>
        where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            // Set the respective properties to the provided values
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; } // Property for the pageIndex
        public int PageSize { get; set; } // Property for the pageSize
        public int Count { get; set; } // Property for the count
        public IReadOnlyList<T> Data { get; set; } // Property for the data
    }
}
