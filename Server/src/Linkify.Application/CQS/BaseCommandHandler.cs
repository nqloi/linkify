using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Bases;
using Linkify.Domain.Interfaces;

namespace Linkify.Application.CQS
{
    public abstract class BaseCommandHandler
    {
        protected readonly ICurrentUserService _currentUserService;

        protected BaseCommandHandler(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        protected Guid GetCurrentUserId() => _currentUserService.GetUserId();
    }

    public abstract class BaseCommandHandler<TEntity, TRepository> : BaseCommandHandler
        where TEntity : BaseEntity, IAggregateRoot
        where TRepository : IBaseCommandRepository<TEntity>
    {
        protected readonly TRepository _repository;
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseCommandHandler(
            TRepository repository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
            : base(currentUserService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
    }
}
