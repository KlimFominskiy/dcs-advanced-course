namespace Loans.Application.DataAccess.Models;

/// <summary>
/// Модель клиента для хранилища.
/// </summary>
public class ClientEntity
{
    /// <summary>
    /// Идентификационный номер.
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
    public required string LastName { get; init; }
    
    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime BirthDate { get; init; }
    
    /// <summary>
    /// Заработная плата.
    /// </summary>
    public decimal Salary { get; init; }
}