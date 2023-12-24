using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Clients.Repositories;
using Loans.Application.AppServices.Contracts;
using Loans.Application.AppServices.Contracts.Clients.Exceptions;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.AppServices.Contracts.Clients.Validators;
using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Mappers;
using Loans.Application.AppServices.Models;

namespace Loans.Application.AppServices.Clients.Handlers;

/// <inheritdoc/>
internal class ClientRequestHandlers : IClientRequestHandlers
{
    private readonly IClientsRepository _clientsRepository;
    private readonly IClientValidator _clientValidator;
    private readonly IMapper<Client, ClientDto> _clientToClientDtoMapper;
    private readonly IMapper<ClientDto, Client> _clientDtoToClientMapper;

    
    public ClientRequestHandlers(IClientsRepository clientsRepository, IClientValidator clientValidator,
        IMapper<Client, ClientDto> clientToClientDtoMapper, IMapper<ClientDto, Client> clientDtoToClientMapper)
    {
        _clientsRepository = clientsRepository ?? throw new ArgumentNullException(nameof(clientsRepository));
        _clientValidator = clientValidator ?? throw new ArgumentNullException(nameof(clientValidator));
        _clientToClientDtoMapper = clientToClientDtoMapper ?? throw new ArgumentNullException(nameof(clientToClientDtoMapper));
        _clientDtoToClientMapper = clientDtoToClientMapper ?? throw new ArgumentNullException(nameof(clientDtoToClientMapper));
    }

    public async Task<ClientDto> GetClientById(long clientId, CancellationToken cancellationToken)
    {
        Client client = await _clientsRepository.GetClientById(clientId, cancellationToken);
        ClientDto clientDto = _clientToClientDtoMapper.Map(client);

        return clientDto;
    }
    
    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<ClientDto>> GetClientsByFilter(ClientFilter clientFilter, CancellationToken cancellationToken)
    {
        if (clientFilter == null) throw new ArgumentNullException(nameof(clientFilter));
        
        IReadOnlyCollection<Client> clientFilteredEntities = await _clientsRepository.GetClientsByFilter(clientFilter, cancellationToken);
        IReadOnlyCollection <ClientDto> clientFilteredDtos = clientFilteredEntities
            .Select(clientInfo => _clientToClientDtoMapper.Map(clientInfo))
            .ToList()
            .AsReadOnly();
        
        return clientFilteredDtos;
    }

    /// <inheritdoc/>
    public async Task<long> CreateClient(string firstName, string middleName, string lastName, DateTime birthDate,
        decimal salary, CancellationToken cancellationToken)
    {
        ClientDto clientDto = new()
        {
            Id = default,
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            BirthDate = birthDate,
            Salary = salary
        };
        List<string> validationErrors = _clientValidator.Validate(clientDto);
        if (validationErrors.Count > 0)
        {
            throw new ClientValidationException(validationErrors);
        }

        Client client = _clientDtoToClientMapper.Map(clientDto);
        long clientId = await _clientsRepository.CreateClient(client, cancellationToken);
        
        return clientId;
    }
}
