using System.Net.Sockets;
using Loans.Application.AppServices.Contracts.Loans.Entities;

namespace Loans.Application.AppServices.Contracts.Clients.Entities;

/// <summary>
/// Сущность клиента
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public long Id { get; set; }

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

    /// <summary>
    /// Навигационное свойство для всех кредитов у данного клиента
    /// </summary>
    public virtual List<Loan> Loans { get; set; }

    public override string ToString()
    {
        return $"{LastName} {FirstName} {MiddleName}, {BirthDate}, {Salary}";
    }
}
