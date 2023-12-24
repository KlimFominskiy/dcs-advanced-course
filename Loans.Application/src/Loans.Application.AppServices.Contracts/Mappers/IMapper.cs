namespace Loans.Application.AppServices.Contracts.Mappers;

/// <summary>
/// Обобщенный класс Mapper для отображения свойств объекта исходного типа в объект целевого типа.
/// </summary>
/// <typeparam name="TSource">Тип исходного объекта.</typeparam>
/// <typeparam name="TDestination">Тип целевого объекта.</typeparam>
public interface IMapper<TSource, TDestination>
{
    /// <summary>
    /// Отображает свойства объекта исходного типа в объект целевого типа.
    /// </summary>
    /// <param name="source">Исходный объект для отображения.</param>
    /// <returns>Объект целевого типа с отображенными свойствами.</returns>
    public TDestination Map(TSource source);
}