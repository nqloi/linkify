using Linkify.Domain.Bases;
using Linkify.Domain.Constants;
using Linkify.Domain.Interfaces;

namespace Linkify.Domain.Aggregates.Token
{
    public class Token : BaseEntity, IAggregateRoot
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = null!;
        public DateTimeOffset ExpiryDate { get; set; }

        public Token() : base() { }

        public Token(
            Guid userId,
            string refreshToken
        )
        {
            UserId = userId;
            RefreshToken = refreshToken.Trim();
            ExpiryDate = DateTime.UtcNow.AddDays(TokenConst.ExpiryInDays);
        }
    }
}
