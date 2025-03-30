using Linkify.Application.ExternalServices;
using Linkify.Application.Repositories;
using Linkify.Domain.Aggregates.PostAggregate;
using Linkify.Domain.Bases;
using Linkify.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.CQS
{
    public abstract class BaseCommandHandler<TEntity>(
        IBaseCommandRepository<TEntity> repository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService) where TEntity : BaseEntity, IAggregateRoot
    {
        protected readonly IBaseCommandRepository<TEntity> _repository = repository;
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly ICurrentUserService _currentUserService = currentUserService;
    }
}
