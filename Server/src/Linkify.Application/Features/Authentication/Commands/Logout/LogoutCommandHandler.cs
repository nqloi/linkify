using ErrorOr;
using Linkify.Application.Repositories;
using MediatR;

namespace Linkify.Application.Features.Authentication.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ErrorOr<bool>>
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutCommandHandler(ITokenRepository tokenRepository, IUnitOfWork unitOfWork)
    {
        _tokenRepository = tokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        var tokens = await _tokenRepository.GetByUserIdAsync(command.UserId, cancellationToken);
        foreach (var token in tokens)
        {
            _tokenRepository.Purge(token);
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
