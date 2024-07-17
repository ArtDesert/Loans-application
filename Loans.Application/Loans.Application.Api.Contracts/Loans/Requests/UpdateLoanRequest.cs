using Loans.Application.Api.Contracts.Loans.Enums;

namespace Loans.Application.Api.Contracts.Loans.Requests;

/// <summary>
/// Входная модель для обновления кредита
/// </summary>
public class UpdateLoanRequest
{
    /// <summary>
    /// Идентификатор кредита
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Сумма кредита
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Срок
    /// </summary>
    public int PeriodInMonth { get; set; }

    /// <summary>
    /// Ставка
    /// </summary>
    public decimal InterestRate { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Статус кредита
    /// </summary>
    public LoanStatus Status { get; set; }

    /// <summary>
    /// Причина отказа
    /// </summary>
    public string? DenialReason { get; set; }

}
