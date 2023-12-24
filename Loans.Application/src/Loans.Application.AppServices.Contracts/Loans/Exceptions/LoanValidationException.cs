namespace Loans.Application.AppServices.Contracts.Loans.Exceptions;

/// <summary>
/// Ошибка валидации данных кредитного договора.
/// </summary>
public class LoanValidationException : Exception
{
    /// <summary>
    /// Ошибки валидации кредитного договора.
    /// </summary>
    public List<string> ValidationErrors { get; }

    /// <summary>
    /// Инициализация нового экземпляра класса LoanValidationException с указанными ошибками валидации.
    /// </summary>
    /// <param name="validationErrors">Список ошибок валидации.</param>
    public LoanValidationException(List<string> validationErrors) : base ($"Ошибка валидации данных кредитного договора. {validationErrors}")
    {
        ValidationErrors = validationErrors;
    }
}