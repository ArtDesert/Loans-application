using DCS.DecisionMakerService.Client.Kafka.Events;
using DCS.Platform.Kafka.Abstractions.Helpers;
using KafkaFlow;
using KafkaFlow.Serializer;
using KafkaFlow.TypedHandler;
using Loans.Application.AppServices.Extensions;
using Loans.Application.AppServices.Kafka.Abstractions;
using Loans.Application.AppServices.Options;
using Loans.Application.DataAccess.Extensions;
using Loans.Application.DataAccess.Providers;
using Loans.Application.Host.Kafka;
using Loans.Application.Host.Kafka.Producers;
using Microsoft.EntityFrameworkCore;

namespace Loans.Application.Host.Extensions;

public static class ServiceCollectionExtensions
{

	public static IServiceCollection AddCustomServices(this IServiceCollection services)
	{
		return services.AddValidators()
					   .AddHandlers()
					   .AddRepositories();
	}

	public static IServiceCollection ConfigureCustomOptions(this IServiceCollection services, IConfiguration configuration)
	{
		return services.Configure<LoanRestrictionOptions>(configuration.GetSection(nameof(LoanRestrictionOptions)))
					   .Configure<ServiceOptions>(configuration.GetSection(nameof(ServiceOptions)))
					   .Configure<DecisionMakerServiceOptions>(configuration.GetSection(nameof(DecisionMakerServiceOptions)))
					   .Configure<KafkaOptions>(configuration.GetSection(nameof(KafkaOptions)));
	}

	public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		return services.AddDbContext<LoansApplicationContext>(optionsBuilder =>
		{
			optionsBuilder.UseNpgsql(configuration.GetConnectionString("loans-application-service-db-connection"))
						  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});
	}

	public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IDecisionMakerProducer, DecisionMakerProducer>()
				.AddScoped<IMessageHandler<CalculateDecisionEventResult>, DecisionMakerConsumer>();
		var kafkaOptions = configuration.GetSection(nameof(KafkaOptions)).Get<KafkaOptions>();
        if (kafkaOptions == null)
		{
			throw new ArgumentException($"Не удалось найти конфигурацию для Kafka: {nameof(kafkaOptions)} было null");
		}
		else if (kafkaOptions.Servers == null || string.IsNullOrWhiteSpace(kafkaOptions.ConsumerGroup))
		{
			throw new ArgumentException($"Конфигурация для Kafka пустая: {nameof(kafkaOptions.ConsumerGroup)} или {nameof(kafkaOptions.Servers)} было null");
		}
		return services
			.AddKafka(kafka => kafka
				.UseMicrosoftLog()
				.AddCluster(cluster => cluster
					.WithBrokers(kafkaOptions.Servers)
					.AddProducer<DecisionMakerProducer>(producer =>
					{
						producer.AddMiddlewares(middlewares => middlewares
							.AddSerializer<JsonCoreSerializer>());
						producer.DefaultTopic(KafkaHelpers.GetTopic(typeof(CalculateDecisionEvent)));
					})
					.AddConsumer(consumer =>
					{
						consumer.Topic(KafkaHelpers.GetTopic(typeof(CalculateDecisionEventResult)));
						consumer.WithGroupId(kafkaOptions.ConsumerGroup);
						consumer.WithBufferSize(100);
						consumer.WithWorkersCount(10);
						consumer.AddMiddlewares(middlewares =>
						{
							middlewares.AddSerializer<JsonCoreSerializer>();
							middlewares.AddTypedHandlers(handlers =>
							{
								handlers.WithHandlerLifetime(InstanceLifetime.Scoped);
								handlers.AddHandler<DecisionMakerConsumer>();
								handlers.WhenNoHandlerFound(context =>
								{
									Console.WriteLine($"Message not handled > Partition: {0} | Offset: {1}",
										context.ConsumerContext.Partition,
										context.ConsumerContext.Offset);
								});
							});
						});
					})
				)
			);
	}
}
