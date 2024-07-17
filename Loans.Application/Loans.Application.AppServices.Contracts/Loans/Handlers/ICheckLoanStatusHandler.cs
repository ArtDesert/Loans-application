using Loans.Application.Api.Contracts.Loans.Enums;

namespace Loans.Application.AppServices.Contracts.Loans.Handlers;

public interface ICheckLoanStatusHandler
{
    Task<LoanStatus> HandleAsync(long loanId, CancellationToken token = default);
}
