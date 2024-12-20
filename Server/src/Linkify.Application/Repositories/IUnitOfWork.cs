﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync(CancellationToken cancellationToken = default);
        void Save();
    }
}
