namespace Loans.Application.Api.Contracts.Clients.Requests;

/// <summary>
/// Входная модель для обновления клиента
/// </summary>
public class UpdateClientRequest
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Зарплата
    /// </summary>
    public decimal Salary { get; set; }
}
