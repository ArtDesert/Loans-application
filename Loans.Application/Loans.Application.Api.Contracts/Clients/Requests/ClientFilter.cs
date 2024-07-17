namespace Loans.Application.Api.Contracts.Clients.Requests;

/// <summary>
/// Входная модель для фильтрации клиентов. Отличается от входной модели для создания нового клиента наличием опциональных свойств.
/// </summary>
public class ClientFilter
{
    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; init; }

    /// <summary>
    /// Нижняя граница даты рождения
    /// </summary>
    public DateTime? BirthDateLowerBound { get; init; }

    /// <summary>
    /// Верхняя граница даты рождения
    /// </summary>
    public DateTime? BirthDateUpperBound { get; init; }

    /// <summary>
    /// Нижняя граница зарплаты
    /// </summary>
    public decimal? SalaryLowerBound { get; init; }
    
    /// <summary>
    /// Верхняя граница зарплаты
    /// </summary>
    public decimal? SalaryUpperBound { get; init; }
}
