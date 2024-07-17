using Loans.Application.AppServices.Contracts.BaseContracts;
using Loans.Application.AppServices.Contracts.Clients.Entities;

namespace Loans.Application.AppServices.Contracts.Clients.Repositories;

public interface IClientRepository : IBaseRepository<Client, long>
{

}
