using Loans.Application.Api.Contracts.Abstractions.Controllers;
using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.Api.Contracts.Clients.Responses;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Loans.Application.Host.Controllers;

[ApiController]
[Route("clients")]
public class ClientController : IClientController
{

	private readonly ICreateClientHandler _сreateClientHandler;
	private readonly IFilterClientsHandler _filterClientsHandler;

	public ClientController(ICreateClientHandler сreateClientHandler, IFilterClientsHandler filterClientsHandler)
	{
		_сreateClientHandler = сreateClientHandler ?? throw new ArgumentNullException(nameof(сreateClientHandler));
		_filterClientsHandler = filterClientsHandler ?? throw new ArgumentNullException(nameof(filterClientsHandler));
	}

	[HttpPost("create")]
	public async Task<long> CreateClientAsync(
		[FromBody] CreateClientRequest clientRequest, 
		CancellationToken token = default)
	{
		return await _сreateClientHandler.HandleAsync(clientRequest, token);
	}

	[HttpPost("filter")]
	public async Task<IEnumerable<ClientResponse>> FilterClientsAsync(
		[FromBody] ClientFilter template, 
		CancellationToken token = default)
	{
		return await _filterClientsHandler.HandleAsync(template, token);
	}
}
