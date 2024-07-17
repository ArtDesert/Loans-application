namespace Loans.Application.Api.Contracts.Clients.Requests;

/// <summary>
/// Входная модель для создания нового клиента
/// </summary>
public record CreateClientRequest
{
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; init; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; init; }

    /// <summary>
    /// Зарплата
    /// </summary>
    public decimal Salary { get; init; }
}
