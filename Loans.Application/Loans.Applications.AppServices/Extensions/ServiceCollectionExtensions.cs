using Loans.Application.AppServices.Clients.Handlers;
using Loans.Application.AppServices.Clients.Validators;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.AppServices.Contracts.Clients.Validators;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Validators;
using Loans.Application.AppServices.Kafka.Abstractions;
using Loans.Application.AppServices.Kafka.Classes;
using Loans.Application.AppServices.Loans.Handlers;
using Loans.Application.AppServices.Loans.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace Loans.Application.AppServices.Extensions;
public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddValidators(this IServiceCollection services) 
	{
		return services.AddScoped<ILoanValidator, LoanValidator>()
					   .AddScoped<IClientValidator, ClientValidator>();
	}

	public static IServiceCollection AddHandlers(this IServiceCollection services)
	{
		return services.AddScoped<ICheckLoanStatusHandler, CheckLoanStatusHandler>()
					   .AddScoped<ICreateClientHandler, CreateClientHandler>()
					   .AddScoped<IFilterClientsHandler, FilterClientsHandler>()
					   .AddScoped<IGetClientByIdHandler, GetClientByIdHandler>()
					   .AddScoped<IGetLoanByIdHandler, GetLoanByIdHandler>()
					   .AddScoped<IGetLoanIdsByClientIdHandler, GetLoanIdsByClientIdHandler>()
					   .AddScoped<IGetLoansByClientIdHandler, GetLoansByClientIdHandler>()
					   .AddScoped<ICreateLoanHandler, CreateLoanHandler>()
					   .AddScoped<ICalculateDecisionHandler, CalculateDecisionHandler>();
	}
}
