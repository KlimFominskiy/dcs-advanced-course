namespace Loans.Application.AppServices.Contracts.Clients.Exceptions;

/// <summary>
/// Ошибка валидации данных клиента.
/// </summary>
public class ClientValidationException : Exception
{
    /// <summary>
    /// Ошибки валидации данных клиента.
    /// </summary>
    public List<string> ValidationErrors { get; }
    
    /// <summary>
    /// Инициализация нового экземпляра класса ClientValidationException с указанными ошибками валидации.
    /// </summary>
    /// <param name="validationErrors">Список ошибок валидации.</param>
    public ClientValidationException(List<string> validationErrors) : base ($"Ошибка валидации данных клиента. {validationErrors}")
    {
        ValidationErrors = validationErrors;
    }
}