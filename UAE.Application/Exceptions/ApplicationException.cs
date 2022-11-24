using System;

namespace UAE.Application.Exceptions;

internal class ApplicationException : Exception
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