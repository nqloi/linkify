namespace Linkify.Application.Common.Models
{
    public class OffsetPaginatedResult<T>
    {
        public IReadOnlyList<T> Items { get; init; } = new List<T>();
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        //public PaginationMetadata Metadata { get; init; } = new();
    }
}
