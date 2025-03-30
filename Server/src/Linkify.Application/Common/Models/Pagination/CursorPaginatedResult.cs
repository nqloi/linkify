namespace Linkify.Application.Common.Models
{
    public class CursorPaginatedResult<T>
    {
        public IReadOnlyList<T> Items { get; init; } = new List<T>();
        public string? Cursor { get; init; }
        public bool HasNextPage { get; init; }
    }
}
