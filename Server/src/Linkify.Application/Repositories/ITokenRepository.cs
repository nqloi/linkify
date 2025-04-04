﻿using Linkify.Domain.Aggregates.Token;

namespace Linkify.Application.Repositories
{
    public interface ITokenRepository : IBaseCommandRepository<Token>
    {
        Task<Token> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
        Task<List<Token>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
