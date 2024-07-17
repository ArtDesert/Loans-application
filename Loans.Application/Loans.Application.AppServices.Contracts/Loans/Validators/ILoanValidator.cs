using Loans.Application.Api.Contracts.Loans.Requests;
using Loans.Application.AppServices.Contracts.BaseContracts;

namespace Loans.Application.AppServices.Contracts.Loans.Validators;

public interface ILoanValidator : IBaseValidator<CreateLoanApplicationRequest>
{

}
