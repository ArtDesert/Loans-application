using DCS.DecisionMakerService.Client.Kafka.Events;
using KafkaFlow;
using KafkaFlow.TypedHandler;
using Loans.Application.AppServices.Kafka.Abstractions;
using Loans.Application.AppServices.Kafka.Classes;

namespace Loans.Application.Host.Kafka;

public class DecisionMakerConsumer : IMessageHandler<CalculateDecisionEventResult>
{
	private readonly ICalculateDecisionHandler _calculateDecisionHandler;

	public DecisionMakerConsumer(ICalculateDecisionHandler calculateDecisionHandler)
	{
		_calculateDecisionHandler = calculateDecisionHandler ?? throw new ArgumentNullException(nameof(calculateDecisionHandler));
	}

	public async Task Handle(IMessageContext context, CalculateDecisionEventResult message)
	{
		var calculateDecisionResponse = Map(message);
		await _calculateDecisionHandler.HandleAsync(calculateDecisionResponse);
	}

	private static CalculateDecisionResponse Map(CalculateDecisionEventResult message)
	{
		var calculateDecisionResponse = new CalculateDecisionResponse()
		{
			LoanId = message.ApplicationId,
			Decision = message.Decision
		};
		return calculateDecisionResponse;
    }
}
