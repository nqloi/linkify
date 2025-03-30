using AutoMapper;
using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Bases;

namespace Linkify.Application.CQS
{
    public abstract class BaseQueryHandler<TEntity>(IBaseQueryRepository<TEntity> repository, ICurrentUserService currentUserService, IMapper mapper) where TEntity : BaseEntity
    {
        protected readonly IBaseQueryRepository<TEntity> _repository = repository;
        protected readonly ICurrentUserService _currentUserService = currentUserService;
        protected readonly IMapper _mapper = mapper;
    }
}
