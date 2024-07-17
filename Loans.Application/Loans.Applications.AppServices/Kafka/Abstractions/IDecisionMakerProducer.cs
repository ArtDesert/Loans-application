using DCS.DecisionMakerService.Client.Kafka.Events;

namespace Loans.Application.AppServices.Kafka.Abstractions;

public interface IDecisionMakerProducer
{
    Task SendDecisionRequestEvent(CalculateDecisionEvent eventMessage);
}
