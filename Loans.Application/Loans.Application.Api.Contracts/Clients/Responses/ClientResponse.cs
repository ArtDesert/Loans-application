namespace Loans.Application.Api.Contracts.Clients.Responses;

public record ClientResponse
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; init; }

    /// <summary>
    /// Зарплата
    /// </summary>
    public decimal Salary { get; init; }
}
