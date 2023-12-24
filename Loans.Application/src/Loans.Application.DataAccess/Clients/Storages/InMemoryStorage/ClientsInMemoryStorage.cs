using System.Collections.Concurrent;
using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.DataAccess.Models;
using Loans.Application.DataAccess.Storages.InMemoryStorage;

namespace Loans.Application.DataAccess.Clients.Storages.InMemoryStorage;

/// <inheritdoc/>
internal class ClientsInMemoryStorage : InMemoryStorage<Client, ClientEntity>
{
    public ClientsInMemoryStorage(ConcurrentDictionary<long, ClientEntity> entities, 
        IMapper<Client, ClientEntity> clientToClientEntityMapper) : base(entities, clientToClientEntityMapper)
    {
        
    }
}