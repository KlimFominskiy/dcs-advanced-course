using System.Collections.Concurrent;
using System.Reflection;
using Loans.Application.AppServices.Contracts.Mappers;

namespace Loans.Application.DataAccess.Storages.InMemoryStorage;

internal abstract class InMemoryStorage<TModel, TEntity> : IInMemoryStorage<TModel, TEntity>
{
    private readonly ConcurrentDictionary<long, TEntity> _entities;
    
    private readonly IMapper<TModel, TEntity> _modelToEntityMapper;

    public IEnumerable<TEntity> Values => _entities.Values;
    
    private long _entityId;
    
    public InMemoryStorage(ConcurrentDictionary<long, TEntity> entities, IMapper<TModel, TEntity> modelToEntityMapper)
    {
        _entities = entities ?? throw new ArgumentNullException(nameof(entities));
        _modelToEntityMapper = modelToEntityMapper ?? throw new ArgumentNullException(nameof(modelToEntityMapper));
    }

    public virtual Task Add(TModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }
        
        long id = Interlocked.Increment(ref _entityId);
        PropertyInfo property = typeof(TModel).GetProperty("Id")
                                ?? throw new InvalidOperationException("У класса нет свойства Id");
        property.SetValue(model, id);
        TEntity entity = _modelToEntityMapper.Map(model ?? throw new ArgumentNullException(nameof(model)));
        _entities.TryAdd(id, entity);
        
        return Task.CompletedTask;
    }

    public virtual Task Update(long id, TModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }
        
        TEntity newEntityData = _modelToEntityMapper.Map(model ?? throw new ArgumentNullException(nameof(model)));
        _entities.TryGetValue(_entityId, out TEntity? oldEntityData);
        _entities.TryUpdate(_entityId, newEntityData, oldEntityData ?? throw new InvalidOperationException());
        
        return Task.CompletedTask;
    }

    public virtual Task Delete(long id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }
        
        _entities.TryRemove(id, out TEntity? entityToDelete);
        
        return Task.CompletedTask;
    }
}