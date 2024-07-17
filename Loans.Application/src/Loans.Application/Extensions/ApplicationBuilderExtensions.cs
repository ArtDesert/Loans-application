using KafkaFlow;
using Loans.Application.Host.Middlewares;

namespace Loans.Application.Host.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseResponseEnriching(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ResponseEnrichingMiddleware>();
    }

    public static IApplicationBuilder UseKafkaBus(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
    {
		IKafkaBus kafkaBus = app.ApplicationServices.CreateKafkaBus();
		lifetime.ApplicationStarted.Register(() => kafkaBus.StartAsync(lifetime.ApplicationStopped));
        return app;
	}
}
