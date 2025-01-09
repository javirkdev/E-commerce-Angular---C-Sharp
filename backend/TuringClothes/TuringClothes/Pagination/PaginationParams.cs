using static TuringClothes.Enums.OrderCatalog;

namespace TuringClothes.Pagination
{
    public class PaginationParams
    {
        public string? Query { get; set; }

        private const int MaxPageSize = 24;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 8;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value < MaxPageSize) ? value : MaxPageSize;
        }
        public OrderField OrderBy { get; set; } = OrderField.None;
        public OrderDirection Direction { get; set; } = OrderDirection.Ascending;
    }
}
