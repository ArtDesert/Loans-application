using DCS.DecisionMakerService.Client.Kafka.Events;
using KafkaFlow;
using Loans.Application.AppServices.Kafka.Abstractions;

namespace Loans.Application.Host.Kafka.Producers;

public class DecisionMakerProducer : IDecisionMakerProducer
{
	private readonly IMessageProducer<DecisionMakerProducer> _producer;

	public DecisionMakerProducer(IMessageProducer<DecisionMakerProducer> producer)
	{
		_producer = producer;
	}

	public Task SendDecisionRequestEvent(CalculateDecisionEvent eventMessage)
	{
		return _producer.ProduceAsync(
			messageKey: eventMessage.ClientId.ToString(), 
			messageValue: eventMessage);
	}
}
