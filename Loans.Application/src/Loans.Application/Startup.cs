using KafkaFlow;
using Loans.Application.Host.Extensions;
using Serilog;

namespace Loans.Application.Host;

public class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();

		services.AddEndpointsApiExplorer()
				.AddSwaggerGen()
				.AddHealthChecks();

		services.ConfigureCustomOptions(_configuration)
				.AddCustomServices()
				.AddDbContext(_configuration)
				.AddKafka(_configuration);

		services.AddSerilog(config => config.ReadFrom.Configuration(_configuration));
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
	{
		if (env.IsDevelopment())
		{
			app.UseSwagger()
			   .UseSwaggerUI();
		}
		app.UseKafkaBus(lifetime)
		   .UseRouting()
		   .UseResponseEnriching()
		   .UseHealthChecks("/health")
		   .UseEndpoints(endpoints =>
		   {
			   endpoints.MapControllers();
		   });
	}
}
