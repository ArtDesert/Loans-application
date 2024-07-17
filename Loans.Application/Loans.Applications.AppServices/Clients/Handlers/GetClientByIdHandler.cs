using Loans.Application.AppServices.Contracts.Clients.Entities;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.AppServices.Contracts.Clients.Repositories;
using Microsoft.Extensions.Logging;

namespace Loans.Application.AppServices.Clients.Handlers;

internal class GetClientByIdHandler : IGetClientByIdHandler
{
	private readonly IClientRepository _repository;
	private readonly ILogger<GetClientByIdHandler> _logger;

	public GetClientByIdHandler(IClientRepository repository, ILogger<GetClientByIdHandler> logger)
	{

		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public Task<Client?> HandleAsync(long id, CancellationToken token = default)
	{
		return _repository.GetByIdAsync(id, token);
	}
}
