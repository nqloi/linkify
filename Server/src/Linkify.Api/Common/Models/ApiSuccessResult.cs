namespace Linkify.Api.Common.Models
{
    public class ApiSuccessResult<T>
    {
        public int? Code { get; init; } = StatusCodes.Status200OK;
        public string? Message { get; init; } = "Success";
        public T? Content { get; init; }
    }
}
