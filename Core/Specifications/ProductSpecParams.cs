namespace Core.Specifications
{
    public class ProductSpecParams // This class represents the parameters for filtering and paginating products
    {
        private const int MaxPageSize = 50; // The maximum page size for pagination

        public int PageIndex { get; set; } = 1; // The index of the page to retrieve

        // The size of the page to retrieve, with validation to ensure it does not exceed the maximum page size
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? BrandId { get; set; } // The ID of the brand to filter by

        public int? TypeId { get; set; } // The ID of the type to filter by

        public string? Sort { get; set; } // The field to sort by

        // The search term, with conversion to lowercase
        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = value?.ToLower();
        }
    }
}
