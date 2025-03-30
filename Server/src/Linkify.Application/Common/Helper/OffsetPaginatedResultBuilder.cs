namespace Linkify.Application.Common.Models
{
    public class OffsetPaginatedResultBuilder<T>
    {
        private readonly List<T> _items = new();
        private int _pageNumber = 1;
        private int _pageSize = 10;
        private int _totalCount;

        public OffsetPaginatedResultBuilder<T> WithItems(IEnumerable<T> items)
        {
            _items.Clear();
            _items.AddRange(items);
            return this;
        }

        public OffsetPaginatedResultBuilder<T> WithPagination(
            int pageNumber,
            int pageSize,
            int totalCount)
        {
            _pageNumber = pageNumber < 1 ? 1 : pageNumber;
            _pageSize = pageSize < 1 ? 10 : pageSize;
            _totalCount = totalCount < 0 ? 0 : totalCount;
            return this;
        }

        public OffsetPaginatedResult<T> Build()
        {
            var totalPages = (int)Math.Ceiling(_totalCount / (double)_pageSize);

            return new OffsetPaginatedResult<T>
            {
                Items = _items.AsReadOnly(),
                PageNumber = _pageNumber,
                PageSize = _pageSize,
                TotalCount = _totalCount,
                TotalPages = totalPages,
            };
        }
    }
}
