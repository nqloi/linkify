using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.Token;
using Linkify.Infrastructure.DataAccessManagers.Context;
using Linkify.Infrastructure.DataAccessManagers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Linkify.Infrastructure.DataAccessManagers.Repositories
{
    public class TokenRepository(ApplicationDbContext _context) : BaseCommandRepository<Token>(_context), ITokenRepository
    {
        public async Task<Token> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            var entity = await _context.Token
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken);


            if (entity == null)
            {
                throw new TokenRepositoryException($"Refresh token has expired, please re-login. {refreshToken}");
            }
            return entity;
        }

        public async Task<List<Token>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var entities = await _context.Token
            .Where(x => x.UserId == userId.ToString())
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            return entities;
        }
    }
}
