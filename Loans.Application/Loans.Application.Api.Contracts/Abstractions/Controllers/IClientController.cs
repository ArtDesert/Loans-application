using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.Api.Contracts.Clients.Responses;

namespace Loans.Application.Api.Contracts.Abstractions.Controllers;

/// <summary>
/// Интерфейс контроллера для работы с сущностью клиента.
/// </summary>
public interface IClientController 
{
	/// <summary>
	/// Создаёт нового клиента
	/// </summary>
	/// <param name="client">Модель для создания клиента</param>
	/// <returns>Идентификатор созданного клиента</returns>
	Task<long> CreateClientAsync(CreateClientRequest client, CancellationToken token);

	/// <summary>
	/// Фильтрует клиентов в соответствии с моделью для фильтрации
	/// </summary>
	/// <param name="template">Модель для фильтрации клиентов</param>
	/// <returns>Коллекция отфильтрованных клиентов</returns>
	Task<IEnumerable<ClientResponse>> FilterClientsAsync(ClientFilter template, CancellationToken token);
}
