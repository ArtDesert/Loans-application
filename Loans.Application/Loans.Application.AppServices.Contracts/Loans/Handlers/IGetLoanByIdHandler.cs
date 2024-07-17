using Loans.Application.Api.Contracts.Loans.Responses;

namespace Loans.Application.AppServices.Contracts.Loans.Handlers;

public interface IGetLoanByIdHandler
{
    Task<LoanResponse> HandleAsync(long id, CancellationToken token = default);
}
