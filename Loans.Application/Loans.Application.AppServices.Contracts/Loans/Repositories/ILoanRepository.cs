using Loans.Application.Api.Contracts.Loans.Requests;
using Loans.Application.Api.Contracts.Loans.Responses;
using Loans.Application.AppServices.Contracts.BaseContracts;
using Loans.Application.AppServices.Contracts.Loans.Entities;

namespace Loans.Application.AppServices.Contracts.Loans.Repositories;

public interface ILoanRepository : IBaseRepository<Loan, long>
{
	Task<IEnumerable<LoanResponse>> GetLoansByClientIdAsync(long clientId, CancellationToken token = default);
}
