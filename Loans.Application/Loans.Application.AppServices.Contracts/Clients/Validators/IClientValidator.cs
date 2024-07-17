using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.AppServices.Contracts.BaseContracts;

namespace Loans.Application.AppServices.Contracts.Clients.Validators;

public interface IClientValidator : IBaseValidator<CreateClientRequest>
{

}
