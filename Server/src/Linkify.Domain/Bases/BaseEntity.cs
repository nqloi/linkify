﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Bases
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private init; }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }

        protected BaseEntity()
        {
            Id = new Guid();
        }
    }
}