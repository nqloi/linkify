﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.SecurityManagers.Identity
{
    public class IdentityException : Exception
    {
        public IdentityException() { }

        public IdentityException(string message) : base(message) { }

        public IdentityException(string message, Exception innerException) : base(message, innerException) { }
    }
}
