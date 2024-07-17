using Loans.Application.AppServices.Contracts.Clients.Entities;

namespace Loans.Application.AppServices.Contracts.Clients.Handlers;
public interface IGetClientByIdHandler
{
    Task<Client?> HandleAsync(long id, CancellationToken token = default);
}
