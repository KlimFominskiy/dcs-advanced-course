namespace Loans.Application.Api.Contracts.Enums;

/// <summary>
/// Статусы кредитного договора.
/// </summary>
public enum LoanStatus
{
    /// <summary>
    /// Неопределено.
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// В работе.
    /// </summary>
    InProgress = 1,
    
    /// <summary>
    /// Одобрен.
    /// </summary>
    Approved = 2,
    
    /// <summary>
    /// Отклонён.
    /// </summary>
    Denied = 3
}