using Loans.Application.AppServices.Contracts.Clients.Repositories;
using Loans.Application.AppServices.Contracts.Loans.Repositories;
using Loans.Application.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Application.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services.AddScoped<IClientRepository, ClientRepository>()
                       .AddScoped<ILoanRepository, LoanRepository>();
    }
}
