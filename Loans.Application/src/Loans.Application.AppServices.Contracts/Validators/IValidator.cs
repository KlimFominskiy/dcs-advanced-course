namespace Loans.Application.AppServices.Contracts.Validators;

/// <summary>
/// Валидатор модели.
/// </summary>
/// <typeparam name="T">Тип модели для валидации.</typeparam>
public interface IValidator<T>
{
    /// <summary>
    /// Валидация данных.
    /// </summary>
    /// <param name="model">Данные для валидации.</param>
    /// <returns>Список ошибок валидации.</returns>
    List<string> Validate(T model);
}