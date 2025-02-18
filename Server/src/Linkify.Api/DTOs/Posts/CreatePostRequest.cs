namespace Linkify.Api.DTOs.Posts
{
    public record CreatePostRequest
    {
        public IEnumerable<IFormFile>? Images { get; set; }
        public string? Content { get; set; } 
    }
}
