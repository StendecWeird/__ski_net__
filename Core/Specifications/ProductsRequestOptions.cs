namespace Core.Specifications
{
    public class ProductsRequestOptions
    {
        public string? Sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }

        private string? _search;
        public string? Search
        {
            get => _search;
            set => _search = value?.ToLower();
        }

        private const int MinPageIndex = 1;
        private int _pageIndex = MinPageIndex;
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                if(value < MinPageIndex)
                    value = MinPageIndex;

                _pageIndex = value;
            }
        }

        private const int MaxPageSize = 50;
        private const int MinPageSize = 3;
        private int _pageSize = MinPageSize;
        public int PageSize 
        {
            get => _pageSize;
            set
            {
                if(value < MinPageSize)
                    value = MinPageSize;

                if(value > MaxPageSize)
                    value = MaxPageSize;

                _pageSize = value;
            }
        }
    }
}