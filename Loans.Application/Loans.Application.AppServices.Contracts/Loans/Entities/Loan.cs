using Loans.Application.Api.Contracts.Loans.Enums;
using Loans.Application.AppServices.Contracts.Clients.Entities;

namespace Loans.Application.AppServices.Contracts.Loans.Entities;

/// <summary>
/// Кредит
/// </summary>
public class Loan
{
    /// <summary>
    /// Идентификатор кредита
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор клиента, оформившего кредит
    /// </summary>
    public long ClientId { get; set; }

    /// <summary>
    /// Навигационное свойство клиента
    /// </summary>
    public virtual Client Client { get; set; }

    /// <summary>
    /// Сумма кредита
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Срок кредита в месяцах
    /// </summary>
    public int PeriodInMonth { get; set; }

    /// <summary>
    /// Процентная ставка
    /// </summary>
    public decimal InterestRate { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    public LoanStatus Status { get; set; }

    /// <summary>
    /// Причина оказа
    /// </summary>
    public string? DenialReason { get; set; }

    public override string ToString()
    {
        return $"{Amount}, {PeriodInMonth}, {InterestRate}, {CreationDate}, {Status}";
    }
}
