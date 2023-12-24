using Loans.Application.Api.Contracts.Enums;

namespace Loans.Application.DataAccess.Models;

/// <summary>
/// Модель кредита для хранилища.
/// </summary>
public class LoanEntity
{
    /// <summary>
    /// Id кредитного договора.
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Id клиента.
    /// </summary>
    public long ClientId { get; init; }
    
    /// <summary>
    /// Сумма кредита.
    /// </summary>
    public decimal Amount { get; init; }
    
    /// <summary>
    /// Срок кредита в месяцах.
    /// </summary>
    /// <returns></returns>
    public int LoanTermInMonths { get; init; }
    
    /// <summary>
    /// Процентная ставка.
    /// </summary>
    public decimal ExpectedInterestRate { get; init; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreationDate { get; init; }
    
    /// <summary>
    /// Статус кредита.
    /// </summary>
    public LoanStatus Status { get; init; }

    /// <summary>
    /// Причина отказа.
    /// </summary>
    public string RejectionReason { get; init; } = "";
    
    /// <summary>
    /// Сумма ежемесячного платежа.
    /// </summary>
    public decimal AnnuityAmount { get; init; }
}