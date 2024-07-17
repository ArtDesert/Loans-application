using DCS.DecisionMakerService.Client.Kafka.Models;

namespace Loans.Application.AppServices.Kafka.Classes;

public class CalculateDecisionResponse
{
    public long LoanId { get; set; }
    public Decision Decision { get; set; }
}
