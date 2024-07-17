using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.DataAccess.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Loans.Application.AppServices.Loans.Handlers;

internal class GetLoanIdsByClientIdHandler : IGetLoanIdsByClientIdHandler
{
	private readonly LoansApplicationContext _context;
	private readonly ILogger<GetLoanIdsByClientIdHandler> _logger;

	public GetLoanIdsByClientIdHandler(LoansApplicationContext context, ILogger<GetLoanIdsByClientIdHandler> logger)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async Task<ICollection<long>> HandleAsync(long clientId, CancellationToken token = default)
	{
		return await _context.Loans
			.Where(x => x.ClientId == clientId)
			.Select(x => x.Id)
			.ToListAsync(token);
	}
}
