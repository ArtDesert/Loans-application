using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.AppServices.Contracts.Clients.Entities;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.AppServices.Contracts.Clients.Repositories;
using Loans.Application.AppServices.Contracts.Clients.Validators;
using Microsoft.Extensions.Logging;

namespace Loans.Application.AppServices.Clients.Handlers;

internal class CreateClientHandler : ICreateClientHandler
{
    private readonly IClientValidator _validator;
    private readonly IClientRepository _repository;
    private readonly ILogger<CreateClientHandler> _logger;

    public CreateClientHandler(IClientValidator validator, IClientRepository repository, ILogger<CreateClientHandler> logger)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

    public async Task<long> HandleAsync(CreateClientRequest clientRequest, CancellationToken token = default)
    {
        _logger.LogDebug("Start operation: create client.");
		_validator.Validate(clientRequest, token);
		var newClient = new Client()
		{
			LastName = clientRequest.LastName,
			FirstName = clientRequest.FirstName,
			MiddleName = clientRequest.MiddleName,
			BirthDate = clientRequest.BirthDate,
			Salary = clientRequest.Salary
		};
		long result = await _repository.CreateAsync(newClient, token);
		_logger.LogInformation("Client created with ID = {id}", result);
        return result;
    }
}
