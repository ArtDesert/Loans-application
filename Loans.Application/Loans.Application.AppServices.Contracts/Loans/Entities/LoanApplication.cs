namespace Loans.Application.AppServices.Contracts.Loans.Entities;

/// <summary>
/// Заявка на кредит
/// </summary>
public record LoanApplication
{

    /// <summary>
    /// Идентификатор заявки
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор клиента, подавшего заявку
    /// </summary>
    public long ClientId { get; set; }

    /// <summary>
    /// Зарплата клиента
    /// </summary>
    public decimal Salary { get; set; }

    /// <summary>
    /// Желаемая сумма кредита
    /// </summary>
    public decimal DesiredAmount { get; set; }

    /// <summary>
    /// Желаемый срок кредита
    /// </summary>
    public int PeriodInMonth { get; set; }
}
