namespace Loans.Application.AppServices.Contracts.Loans.Exceptions;

public class LoanValidationException : Exception
{
    public Exception Inner { get; set; }

    public LoanValidationException(string message, Exception inner = null) : base(message, inner)
    {
        Inner = inner;
    }
}
