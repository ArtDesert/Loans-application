using Loans.Application.Api.Contracts.Clients.Requests;

namespace Loans.Application.AppServices.Contracts.Clients.Handlers;
public interface ICreateClientHandler
{
    Task<long> HandleAsync(CreateClientRequest clientRequest, CancellationToken token = default);
}
