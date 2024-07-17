using Loans.Application.Api.Contracts.Loans.Responses;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Repositories;
using Microsoft.Extensions.Logging;

namespace Loans.Application.AppServices.Loans.Handlers;

internal class GetLoanByIdHandler : IGetLoanByIdHandler
{
	private readonly ILoanRepository _repository;
	private readonly ILogger<GetLoanByIdHandler> _logger;

	public GetLoanByIdHandler(ILoanRepository repository, ILogger<GetLoanByIdHandler> logger)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async Task<LoanResponse> HandleAsync(long id, CancellationToken token = default)
	{
		var result = await _repository.GetByIdAsync(id, token);
		return new LoanResponse
		{
			Id = result.Id,
			ClientId = result.ClientId,
			Amount = result.Amount,
			PeriodInMonth = result.PeriodInMonth,
			InterestRate = result.InterestRate,
			CreationDate = result.CreationDate,
			Status = result.Status
		};
	}
}
