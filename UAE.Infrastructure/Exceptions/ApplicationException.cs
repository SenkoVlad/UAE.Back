using System;

namespace UAE.Infrastructure.Exceptions;

public class ApplicationException : Exception
{
    internal ApplicationException(string businessMessage)
        : base(businessMessage)
    {
    }

    internal ApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}