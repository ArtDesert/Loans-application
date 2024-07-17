using Loans.Application.Api.Contracts.Loans.Requests;
using Loans.Application.Api.Contracts.Loans.Responses;
using Loans.Application.AppServices.Contracts.Clients.Entities;

namespace Loans.Application.AppServices.Contracts.Loans.Handlers;

public interface ICreateLoanHandler
{
    Task<long> HandleAsync(CreateLoanApplicationRequest loanApplication, Client applicant, CancellationToken token = default);
}
