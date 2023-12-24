using Loans.Application.Api.Contracts.Enums;

namespace Loans.Application.Api.Contracts.Dto;

/// <summary>
/// Модель данных кредита для обработки.
/// </summary>
public class LoanDto
{
    /// <summary>
    /// Id кредитного договора.
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// Модель данных клиента для обработки.
    /// </summary>
    public required ClientDto Client { get; init; }

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