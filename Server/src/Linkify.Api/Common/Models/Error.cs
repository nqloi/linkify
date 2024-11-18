﻿// ----------------------------------------------------------------------------
// Developer:      Ismail Hamzah
// Email:         go2ismail@gmail.com
// ----------------------------------------------------------------------------

namespace WebAPI.Common.Models;

public class Error
{
    public string Ref { get; set; }
    public string? ExceptionType { get; init; }
    public string? InnerException { get; init; }
    public string? Source { get; init; }
    public string? StackTrace { get; init; }

    public Error(
    string? innerException,
    string? source,
    string? stackTrace,
    string? exceptionType)
    {
        Ref = "https://datatracker.ietf.org/doc/html/rfc9110";
        InnerException = innerException;
        Source = source?.Trim();
        StackTrace = stackTrace?.Trim();
        ExceptionType = exceptionType;
    }
}
