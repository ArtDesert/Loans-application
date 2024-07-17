namespace Loans.Application.Api.Contracts.Loans.Enums;

/// <summary>
/// Статус кредита
/// </summary>
public enum LoanStatus
{
    /// <summary>
    /// Неизвестно
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// На рассмотрении
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Одобрено
    /// </summary>
    Approved = 2,

    /// <summary>
    /// Отказано
    /// </summary>
	Denied = 3,
    
    /// <summary>
    /// Требуется ручная проверка
    /// </summary>
	Underwriting = 4,
}
