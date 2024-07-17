namespace Loans.Application.AppServices.Contracts.Clients.Exceptions;

public class ClientValidationException : Exception
{
    public Exception Inner { get; set; }

    public ClientValidationException(string message, Exception inner = null) : base(message, inner)
    {
        Inner = inner;
    }
}
