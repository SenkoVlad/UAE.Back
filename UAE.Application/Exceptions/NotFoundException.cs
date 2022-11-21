namespace UAE.Application.Exceptions;

internal sealed class NotFoundException : ApplicationException 
{
    internal NotFoundException(string businessMessage) : base(businessMessage)
    {
    }

    internal NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}