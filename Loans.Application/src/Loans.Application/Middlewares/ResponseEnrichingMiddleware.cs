using Loans.Application.AppServices.Options;
using Microsoft.Extensions.Options;

namespace Loans.Application.Host.Middlewares;

public class ResponseEnrichingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ServiceOptions _serviceOptions;

    public ResponseEnrichingMiddleware(RequestDelegate next, IOptions<ServiceOptions> serviceOptions)
    {
        _next = next;
        _serviceOptions = serviceOptions?.Value ?? throw new ArgumentNullException(nameof(serviceOptions));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!string.IsNullOrWhiteSpace(_serviceOptions.ServiceName))
        {
            context.Response.Headers.Append("Loans.Application.Host", _serviceOptions.ServiceName);
        }
        await _next(context);
    }
}
