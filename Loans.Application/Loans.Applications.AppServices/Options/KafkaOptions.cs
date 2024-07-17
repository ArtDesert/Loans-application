
namespace Loans.Application.AppServices.Options;

public class KafkaOptions
{
	public string[] Servers { get; init; } = Array.Empty<string>();
	public string? ConsumerGroup { get; init; }
}
