using DCS.DecisionMakerService.Client.Kafka.Events;
using Loans.Application.Api.Contracts.Loans.Enums;
using Loans.Application.Api.Contracts.Loans.Requests;
using Loans.Application.AppServices.Contracts.Clients.Entities;
using Loans.Application.AppServices.Contracts.Loans.Entities;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Repositories;
using Loans.Application.AppServices.Contracts.Loans.Validators;
using Loans.Application.AppServices.Kafka.Abstractions;
using Microsoft.Extensions.Logging;

namespace Loans.Application.AppServices.Loans.Handlers;

internal class CreateLoanHandler : ICreateLoanHandler
{
	private readonly ILoanRepository _repository;
	private readonly IDecisionMakerProducer _producer;
	private readonly ILoanValidator _validator;
	private readonly ILogger<CreateLoanHandler> _logger;

	public CreateLoanHandler(ILoanRepository repository, IDecisionMakerProducer producer, 
							 ILoanValidator validator, ILogger<CreateLoanHandler> logger)
	{
		_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		_producer = producer ?? throw new ArgumentNullException(nameof(producer));
		_validator = validator ?? throw new ArgumentNullException(nameof(validator));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}
	public async Task<long> HandleAsync(
		CreateLoanApplicationRequest loanApplication, 
		Client applicant, 
		CancellationToken token = default)
	{
		_logger.LogDebug("Start operation: create loan.");
		_validator.Validate(loanApplication, token);
		var now = DateTime.Now;
		var newLoan = new Loan()
		{
			ClientId = loanApplication.ClientId,
			Amount = loanApplication.DesiredAmount,
			PeriodInMonth = loanApplication.Period,
			InterestRate = 0.1M,
			CreationDate = now,
			Status = LoanStatus.InProgress
		};
		var loanId = await _repository.CreateAsync(newLoan, token);
		_logger.LogInformation("Loan created with ID {loanId}", loanId);
		var query = new CalculateDecisionEvent()
		{
			ApplicationId = loanId,
			ApplicationDate = now,
			CreditAmount = loanApplication.DesiredAmount,
			CreditLenMonth = loanApplication.Period,
			ClientId = loanApplication.ClientId,
			BirthDay = applicant.BirthDate,
			IncomeAmount = applicant.Salary
		};
		await _producer.SendDecisionRequestEvent(query);
		return loanId;
	}
}
