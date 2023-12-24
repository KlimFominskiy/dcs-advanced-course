namespace Loans.Application.DataAccess.Storages.InMemoryStorage;

/// <summary>
/// Методы для работы с данными в хранилище.
/// </summary>
/// <typeparam name="TModel">Модель объекта для бизнес-логики.</typeparam>
/// <typeparam name="TEntity">Модель объекта для базы данных(хранилища).</typeparam>
public interface IInMemoryStorage<TModel, TEntity>
{
    /// <summary>
    /// Перечисление значений объектов в хранилище.
    /// </summary>
    public IEnumerable<TEntity> Values { get; }
    
    /// <summary>
    /// Добавить объект в хранилище.
    /// </summary>
    /// <param name="model">Добавляемые данные.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task Add(TModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить объект в хранилище по Id.
    /// </summary>
    /// <param name="id">Id записи об объекте.</param>
    /// <param name="model">Добавляемые данные.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task Update(long id, TModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить объект из хранилища по Id.
    /// </summary>
    /// <param name="id">Id удаляемой записи.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    public Task Delete(long id, CancellationToken cancellationToken);
}