using Loans.Application.Api.Contracts.Loans.Enums;

namespace Loans.Application.Api.Contracts.Loans.Responses;

/// <summary>
/// Выходная модель кредита, аналогичная входной модели, за исключением причины отказа.
/// </summary>
public record LoanResponse
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
    /// Сумма кредита
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Срок кредита
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
}
