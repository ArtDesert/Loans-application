using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.Api.Contracts.Clients.Responses;

namespace Loans.Application.AppServices.Contracts.Clients.Handlers;

public interface IFilterClientsHandler
{
    Task<IEnumerable<ClientResponse>> HandleAsync(ClientFilter template, CancellationToken token = default);
}
