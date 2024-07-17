namespace Loans.Application.Api.Contracts.Loans.Requests;

/// <summary>
/// Входная модель заявки на кредит
/// </summary>
public record CreateLoanApplicationRequest
{
    /// <summary>
    /// Идентификатор клиента, подавшего заявку
    /// </summary>
    public long ClientId { get; set; }

    /// <summary>
    /// Желаемая сумма кредита
    /// </summary>
    public decimal DesiredAmount { get; set; }

    /// <summary>
    /// Желаемый срок кредита
    /// </summary>
    public int Period { get; set; }
}
