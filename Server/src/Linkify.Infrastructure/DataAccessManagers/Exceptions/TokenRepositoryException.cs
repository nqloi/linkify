using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Infrastructure.DataAccessManagers.Exceptions
{
    public class TokenRepositoryException : Exception
    {
        public TokenRepositoryException() { }

        public TokenRepositoryException(string message) : base(message) { }

        public TokenRepositoryException(string message, Exception innerException) : base(message, innerException) { }
    }
}
