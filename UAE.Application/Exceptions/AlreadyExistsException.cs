using System;

namespace UAE.Application.Exceptions;

internal sealed class AlreadyExistsException : ApplicationException
{
    internal AlreadyExistsException(string businessMessage) : base(businessMessage)
    {
    }

    internal AlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}