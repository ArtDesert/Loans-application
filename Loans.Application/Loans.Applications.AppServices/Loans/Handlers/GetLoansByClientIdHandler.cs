using Loans.Application.Api.Contracts.Loans.Responses;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.DataAccess.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Loans.Application.AppServices.Loans.Handlers;

internal class GetLoansByClientIdHandler : IGetLoansByClientIdHandler
{
	private readonly LoansApplicationContext _context;
	private readonly ILogger<GetLoansByClientIdHandler> _logger;

	public GetLoansByClientIdHandler(LoansApplicationContext context, ILogger<GetLoansByClientIdHandler> logger)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async Task<ICollection<LoanResponse>> HandleAsync(long clientId, CancellationToken token = default)
	{
		return await _context.Loans
			.Where(x => x.ClientId == clientId)
			.Select(loan => new LoanResponse()
			{
				Id = loan.Id,
				ClientId = loan.ClientId,
				Amount = loan.Amount,
				PeriodInMonth = loan.PeriodInMonth,
				InterestRate = loan.InterestRate,
				CreationDate = loan.CreationDate,
				Status = loan.Status
			})
			.ToListAsync(token);
	}
}
