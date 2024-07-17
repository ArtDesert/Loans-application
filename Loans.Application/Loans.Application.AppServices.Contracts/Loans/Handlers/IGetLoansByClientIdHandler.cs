using Loans.Application.Api.Contracts.Loans.Responses;

namespace Loans.Application.AppServices.Contracts.Loans.Handlers;

public interface IGetLoansByClientIdHandler
{
    Task<ICollection<LoanResponse>> HandleAsync(long clientId, CancellationToken token = default);
}
