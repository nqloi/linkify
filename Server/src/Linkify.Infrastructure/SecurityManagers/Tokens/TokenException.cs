﻿namespace Linkify.Infrastructure.SecurityManagers.Tokens
{
    public class TokenException : Exception
    {
        public TokenException() { }

        public TokenException(string message) : base(message) { }

        public TokenException(string message, Exception innerException) : base(message, innerException) { }
    }

}
