namespace UAE.Core.Exceptions;

public sealed class CoreException : Exception
{
    internal CoreException(string businessMessage)
        : base(businessMessage)
    {
    }

    internal CoreException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}