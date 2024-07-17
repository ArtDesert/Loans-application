namespace Loans.Application.AppServices.Contracts.Loans.Handlers;

public interface IGetLoanIdsByClientIdHandler
{
    Task<ICollection<long>> HandleAsync(long clientId, CancellationToken token = default);
}
