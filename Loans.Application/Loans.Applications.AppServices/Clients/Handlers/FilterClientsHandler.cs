using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.Api.Contracts.Clients.Responses;
using Loans.Application.AppServices.Contracts.Clients.Entities;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.DataAccess.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Loans.Application.AppServices.Clients.Handlers;
internal class FilterClientsHandler : IFilterClientsHandler
{
	private readonly LoansApplicationContext _context;
	private readonly ILogger<FilterClientsHandler> _logger;

    public FilterClientsHandler(LoansApplicationContext context, ILogger<FilterClientsHandler> logger)
    {
		_context = context ?? throw new ArgumentNullException(nameof(context));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

    public async Task<IEnumerable<ClientResponse>> HandleAsync(ClientFilter template, CancellationToken token = default)
	{
		_logger.LogDebug("Start operation: filter clients.");
		IQueryable<Client> clients = _context.Clients;
		if (template != null)
		{
			if (!string.IsNullOrWhiteSpace(template.LastName))
			{
				clients = clients.Where(client => client.LastName.ToLower().StartsWith(template.LastName.ToLower()));
			}
			if (!string.IsNullOrWhiteSpace(template.FirstName))
			{
				clients = clients.Where(client => client.FirstName.ToLower().StartsWith(template.FirstName.ToLower()));
			}
			if (!string.IsNullOrWhiteSpace(template.MiddleName))
			{
				clients = clients.Where(client => client.MiddleName.ToLower().StartsWith(template.MiddleName.ToLower()));
			}
			if (template.BirthDateLowerBound != null)
			{
				clients = clients.Where(client => client.BirthDate >= template.BirthDateLowerBound);
			}
			if (template.BirthDateUpperBound != null)
			{
				clients = clients.Where(client => client.BirthDate <= template.BirthDateUpperBound);
			}
			if (template.SalaryLowerBound != null)
			{
				clients = clients.Where(client => client.Salary >= template.SalaryLowerBound);
			}
			if (template.SalaryUpperBound != null)
			{
				clients = clients.Where(client => client.Salary <= template.SalaryUpperBound);
			}
		}
		return await clients
			.Select(client => new ClientResponse()
			{
				Id = client.Id,
				FullName = $"{client.LastName} {client.FirstName} {client.MiddleName}",
				BirthDate = client.BirthDate,
				Salary = client.Salary
			})
			.ToListAsync(token);
	}
}
