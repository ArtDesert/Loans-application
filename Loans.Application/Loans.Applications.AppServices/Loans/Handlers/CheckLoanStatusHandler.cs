using Loans.Application.Api.Contracts.Loans.Enums;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Repositories;
using Microsoft.Extensions.Logging;

namespace Loans.Application.AppServices.Loans.Handlers;

internal class CheckLoanStatusHandler : ICheckLoanStatusHandler
{
	private readonly ILoanRepository _repository;
	private readonly ILogger<CheckLoanStatusHandler> _logger;

	public CheckLoanStatusHandler(ILoanRepository repository, ILogger<CheckLoanStatusHandler> logger)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(_repository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async Task<LoanStatus> HandleAsync(long loanId, CancellationToken token = default)
	{
		var result = await _repository.GetByIdAsync(loanId, token);
		return result.Status;
	}
}
