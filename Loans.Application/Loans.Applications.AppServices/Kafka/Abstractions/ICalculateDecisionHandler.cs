using Loans.Application.AppServices.Kafka.Classes;

namespace Loans.Application.AppServices.Kafka.Abstractions;

public interface ICalculateDecisionHandler
{
    Task HandleAsync(CalculateDecisionResponse calculateDecisionResponse);
}
