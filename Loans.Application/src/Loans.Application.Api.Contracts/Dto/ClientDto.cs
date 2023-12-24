namespace Loans.Application.Api.Contracts.Dto;

/// <summary>
/// Модель данных клиента для обработки.
/// </summary>
public class ClientDto
{
    /// <summary>
    /// Id клиента.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; init; }
    
    /// <summary>
    /// Отчество.
    /// </summary>
    public string? MiddleName { get; init; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public required  string LastName { get; init; }
    
    /// <summary>
    /// Дата рождения клиента.
    /// </summary>
    public DateTime BirthDate { get; init; }
    
    /// <summary>
    /// Заработная плата.
    /// </summary>
    public decimal Salary { get; init; }
}