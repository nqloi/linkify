namespace Linkify.Application.Common.Models
{
    public class OffsetPaginationParameters : IPaginationParameters
    {
        private const int DefaultPageSize = 10;
        private const int MaxPageSize = 50;
        private int _pageSize = DefaultPageSize;
        private int _pageNumber = 1;

        public int PageNumber
        {
            get => _pageNumber;
            init => _pageNumber = value < 1 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            init => _pageSize = value switch
            {
                <= 0 => DefaultPageSize,
                > MaxPageSize => MaxPageSize,
                _ => value
            };
        }

        public int Limit => PageSize;
        public int Offset => (PageNumber - 1) * PageSize;
    }
}
