using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Clients.Handlers;
using Loans.Application.AppServices.Clients.Repositories;
using Loans.Application.AppServices.Contracts.Clients.Exceptions;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.AppServices.Contracts.Clients.Validators;
using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.UnitTests.Data.Clients;
using NSubstitute;
using Xunit;

namespace Loans.Application.UnitTests.Tests.Loans.Application.AppServices.Clients.Handlers
{
    /// <summary>
    /// Тестовый класс для проверки функциональности обработчиков запросов клиентов.
    /// </summary>
    public class ClientRequestHandlersTests
    {
        private readonly IClientRequestHandlers _clientRequestHandlers;
        private readonly IClientsRepository _clientsRepository;
        private readonly IClientValidator _clientValidator;
        private readonly CancellationToken _cancellationToken;
        private readonly ClientDto _clientDto;
        private readonly Client _client;
        private readonly IMapper<Client, ClientDto> _clientToClientDtoMapper;
        private readonly IMapper<ClientDto, Client> _clientDtoToClientMapper;

        /// <summary>
        /// Инициализация объектов перед каждым тестом.
        /// </summary>
        public ClientRequestHandlersTests()
        {
            BuildClient buildClient = new();
            _clientDto = buildClient.GetClientDto();
            _client = buildClient.GetClient();

            _cancellationToken = new CancellationToken();

            _clientValidator = Substitute.For<IClientValidator>();
            
            _clientsRepository = Substitute.For<IClientsRepository>();

            _clientToClientDtoMapper = Substitute.For<IMapper<Client, ClientDto>>();
            
            _clientDtoToClientMapper = Substitute.For<IMapper<ClientDto, Client>>();

            _clientRequestHandlers = new ClientRequestHandlers(_clientsRepository, _clientValidator,
                _clientToClientDtoMapper, _clientDtoToClientMapper);
        }

        /// <summary>
        /// Тест для проверки успешного поиска клиента по его id.
        /// </summary>
        [Fact(DisplayName = "GetClientById. Клиент с таким id найден. Выданы его данные.")]
        public async void GetClientById_ValidId_ClientDto()
        {
            // Arrange
            _clientsRepository.GetClientById(Arg.Any<long>(), _cancellationToken).Returns(_client);
            _clientToClientDtoMapper.Map(Arg.Any<Client>()).Returns(_clientDto);

            // Act
            Client client = await _clientsRepository.GetClientById(1, _cancellationToken);

            // Assert
            Assert.Equivalent(_client, client);
        }
        
        /// <summary>
        /// Тест для проверки успешного получения списка клиентов, удовлетворяющих фильтру.
        /// </summary>
        [Theory(DisplayName = "GetClientsByFilter. Выдан список клиентов, удовлетворяющих фильтру.")]
        [ClassData(typeof(BuildClientFilters))]
        public async void GetClientsByFilter_ValidFilter_FilteredClientsList(ClientFilter clientFilter)
        {
            // Arrange
            List<Client> clients = new()
            {
                _client
            };
            _clientsRepository.GetClientsByFilter(Arg.Any<ClientFilter>(), _cancellationToken).Returns(clients);
            _clientToClientDtoMapper.Map(Arg.Any<Client>()).Returns(_clientDto);

            // Act
            IReadOnlyCollection<ClientDto> clientsByFilter = await
                _clientRequestHandlers.GetClientsByFilter(clientFilter, _cancellationToken);

            // Assert
            Assert.Equivalent(clients, clientsByFilter);
        }

        /// <summary>
        /// Тест для проверки выброса исключения при пустом входном параметре модели для фильтрации.
        /// </summary>
        [Fact(DisplayName = "GetClientsByFilter. Исключение. Пустой входной параметр модели для фильтрации.")]
        public async void GetClientsByFilter_NullClientFilter_ArgumentNullException()
        {
            // Arrange

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
            {
                return _clientRequestHandlers.GetClientsByFilter(null, _cancellationToken);
            });
        }
        
        /// <summary>
        /// Тест для проверки успешного создания клиента и получения его Id.
        /// </summary>
        [Fact(DisplayName = "CreateClient. Клиент успешно создан. Выдан его Id.")]
        public async void CreateClient_ValidClientData_ClientId()
        {
            // Arrange
            _clientValidator.Validate(Arg.Any<ClientDto>()).Returns(new List<string>());
            _clientsRepository.CreateClient(Arg.Any<Client>(), Arg.Any<CancellationToken>()).Returns(_client.Id);
            //Act
            long clientId = await _clientRequestHandlers.CreateClient(_clientDto.FirstName, _clientDto.MiddleName,
                _clientDto.LastName, _clientDto.BirthDate, _clientDto.Salary, _cancellationToken);

            //Assert
            Assert.Equal(_clientDto.Id, clientId);
        }

        /// <summary>
        /// Тест для проверки выброса исключения при невалидных данных клиента.
        /// </summary>
        [Theory(DisplayName = "CreateClient. Исключение. Ошибка валидации данных клиента.")]
        [ClassData(typeof(BuildInvalidClients))]
        public async void CreateClient_InvalidClientData_ClientValidationException(ClientDto clientDto, List<string> validationErrors)
        {
            // Arrange
            _clientValidator.Validate(Arg.Any<ClientDto>()).Returns(validationErrors);
            _clientDtoToClientMapper.Map(Arg.Any<ClientDto>()).Returns(_client);
            _clientsRepository.CreateClient(Arg.Any<Client>(), _cancellationToken).Returns(_client.Id);

            // Act
            Exception? exception = await Record.ExceptionAsync(() =>
                _clientRequestHandlers.CreateClient(clientDto.FirstName, clientDto.MiddleName,
                    clientDto.LastName, clientDto.BirthDate, clientDto.Salary, _cancellationToken));
            
            // Assert
            Assert.IsType<ClientValidationException>(exception);
            Assert.Equivalent( "Ошибка валидации данных клиента. " + validationErrors, exception.Message);
        }
    }
}