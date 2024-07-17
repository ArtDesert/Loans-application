using DCS.DecisionMakerService.Client.Kafka.Enums;
using Loans.Application.Api.Contracts.Loans.Enums;
using Loans.Application.AppServices.Contracts.Loans.Repositories;
using Loans.Application.AppServices.Kafka.Abstractions;

namespace Loans.Application.AppServices.Kafka.Classes;

public class CalculateDecisionHandler : ICalculateDecisionHandler
{
    private readonly ILoanRepository _repository;

    public CalculateDecisionHandler(ILoanRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task HandleAsync(CalculateDecisionResponse calculateDecisionResponse)
    {
        var decision = calculateDecisionResponse.Decision;
		var loan = await _repository.GetByIdAsync(calculateDecisionResponse.LoanId);
        if (loan != null)
        {
			switch (decision.DecisionStatus)
			{
				case DecisionStatus.Unknown:
					loan.Status = LoanStatus.Unknown;
					break;
				case DecisionStatus.Refuse:
					loan.Status = LoanStatus.Denied;
					loan.DenialReason = "unknown reason";
					break;
				case DecisionStatus.Approval:
					loan.Status = LoanStatus.Approved;
					break;
				case DecisionStatus.Underwriting:
					loan.Status = LoanStatus.Underwriting;
					break;
				default:
					break;
			}
			await _repository.UpdateAsync(loan);
		}
		else
		{
            await Console.Out.WriteLineAsync($"There is no credit with id {calculateDecisionResponse.LoanId}");
        }
    }
}
