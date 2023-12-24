using Loans.Application.AppServices.Clients.Repositories;
using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.DataAccess.Models;
using Loans.Application.DataAccess.Storages.InMemoryStorage;

namespace Loans.Application.DataAccess.Clients.Storages.InMemoryStorage.Repositories;

/// <inheritdoc/>
internal class ClientsRepository : IClientsRepository
{
    private readonly IInMemoryStorage<Client, ClientEntity> _clientsInMemoryStorage;
    
    private readonly IMapper<ClientEntity, Client> _clientEntityToClientMapper;
    
    public ClientsRepository(IInMemoryStorage<Client, ClientEntity> clientsMemoryStorage,
        IMapper<ClientEntity, Client> clientEntityToClientMapper)
    {
        _clientsInMemoryStorage = clientsMemoryStorage ?? throw new ArgumentNullException(nameof(clientsMemoryStorage));
        _clientEntityToClientMapper = clientEntityToClientMapper ?? throw new ArgumentNullException(nameof(clientEntityToClientMapper));
    }
    
    /// <inheritdoc/>
    public async Task<long> CreateClient(Client client, CancellationToken cancellationToken)
    {
        await _clientsInMemoryStorage.Add(client, cancellationToken);
        
        return _clientsInMemoryStorage.Values.Last().Id;
    }
    
    /// <inheritdoc/>
    public Task<Client> GetClientById(long clientId, CancellationToken cancellationToken)
    {
        Client client = _clientsInMemoryStorage.Values
            .Where(clientInfo => clientInfo.Id == clientId)
            .Select(clientInfo => _clientEntityToClientMapper.Map(clientInfo))
            .Single();
        
        return Task.FromResult(client);
    }
    
    /// <inheritdoc/>
    public async Task<Client> UpdateClient(long clientId, Client client, CancellationToken cancellationToken)
    {
        await _clientsInMemoryStorage.Update(clientId, client, cancellationToken);
        Client updatedClient = await GetClientById(clientId, cancellationToken);

        return updatedClient;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<Client>> GetClientsByFilter(ClientFilter clientFilter,
        CancellationToken cancellationToken)
    {
        if (clientFilter == null) throw new ArgumentNullException(nameof(clientFilter));
        
        List<Client> filteredClients = _clientsInMemoryStorage.Values
            .Where(clientInfo => 
                (string.IsNullOrWhiteSpace(clientFilter.FirstName) || 
                 clientInfo.FirstName == clientFilter.FirstName) && 
                (string.IsNullOrWhiteSpace(clientFilter.MiddleName) ||
                 clientInfo.MiddleName == clientFilter.MiddleName) && 
                (string.IsNullOrWhiteSpace(clientFilter.LastName) ||
                 clientInfo.LastName == clientFilter.LastName) &&
                (!clientFilter.BirthDate.HasValue ||
                 clientInfo.BirthDate == clientFilter.BirthDate))
            .Select(clientInfo => _clientEntityToClientMapper.Map(clientInfo))
            .ToList();
        
        return await Task.FromResult(filteredClients.AsReadOnly());
    }
}