using Loans.Application.Api.Contracts.Loans.Enums;
using Loans.Application.Api.Contracts.Loans.Requests;
using Loans.Application.Api.Contracts.Loans.Responses;

namespace Loans.Application.Api.Contracts.Abstractions.Controllers;

/// <summary>
/// Интерфейс контроллера для работы с сущностью кредита
/// </summary>
public interface ILoanController
{
	/// <summary>
	/// Создаёт новый кредит
	/// </summary>
	/// <param name="loan">Модель заявки по кредиту</param>
	/// <returns>Идентификатор созданного кредита</returns>
	Task<long> CreateLoanAsync(CreateLoanApplicationRequest loan, CancellationToken token);

	/// <summary>
	/// Возвращает кредит по его идентификатору
	/// </summary>
	/// <param name="id">Идентификатор кредита</param>
	/// <returns></returns>
	Task<LoanResponse> GetLoanByIdAsync(long id, CancellationToken token);

	/// <summary>
	/// Возвращает кредит по идентификатору клиента
	/// </summary>
	/// <param name="clientId">Идентификатор клиента</param>
	/// <returns></returns>
	Task<ICollection<LoanResponse>> GetLoansByClientIdASync(long clientId, CancellationToken token);

	/// <summary>
	/// Возвращает статус кредита
	/// </summary>
	/// <param name="loanId">Идентификатор кредита</param>
	/// <returns></returns>
	Task<LoanStatus> CheckLoanStatus(long loanId, CancellationToken token);

	/// <summary>
	/// Возвращает все кредиты у заданного клиента
	/// </summary>
	/// <param name="clientid">Идентификатор клиента</param>
	/// <returns>Коллекция идентификаторов кредитов, которые оформлены на одного клиента</returns>
	Task<ICollection<long>> GetLoanIdsByClientIdAsync(long clientid, CancellationToken token);
}
