using Loans.Application.AppServices;
using Loans.Application.AppServices.Clients.Repositories;
using Loans.Application.AppServices.Contracts;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Mappers;
using Loans.Application.DataAccess.Clients.Storages.InMemoryStorage.Repositories;
using Loans.Application.DataAccess.Models;
using Loans.Application.DataAccess.Storages.InMemoryStorage;
using Loans.Application.UnitTests.Data.Clients;
using NSubstitute;
using Xunit;

namespace Loans.Application.UnitTests.Tests.Loans.Application.DataAccess.Clients.Storages.InMemoryStorage.Repositories
{
    /// <summary>
    /// Тестовый класс для проверки функциональности репозитория клиентов в памяти.
    /// </summary>
    public class ClientsRepositoryTests
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly CancellationToken _cancellationToken;
        private readonly Client _client;
        private readonly List<Client> _clients;
        private readonly Mapper<ClientEntity, Client> _clientEntityToClientMapper;

        /// <summary>
        /// Инициализация объектов перед каждым тестом.
        /// </summary>
        public ClientsRepositoryTests()
        {
            BuildClient buildClient = new();
            _client = buildClient.GetClient();
            _clients = new()
            {
                _client
            };
            ClientEntity clientEntity = buildClient.GetClientEntity();
            List<ClientEntity> clientEntities = new()
            {
                clientEntity
            };
            
            _cancellationToken = new();
            IInMemoryStorage<Client, ClientEntity> clientsInMemoryStorage = Substitute.For<IInMemoryStorage<Client, ClientEntity>>();
            clientsInMemoryStorage.Values.Returns(clientEntities);
            clientsInMemoryStorage.Add(Arg.Any<Client>(), _cancellationToken).Returns(Task.CompletedTask);
            clientsInMemoryStorage.Update(Arg.Any<long>(), Arg.Any<Client>(), _cancellationToken).Returns(Task.CompletedTask);
            clientsInMemoryStorage.Delete(Arg.Any<long>(), _cancellationToken).Returns(Task.CompletedTask);

            _clientEntityToClientMapper = Substitute.For<Mapper<ClientEntity, Client>>();
            
            _clientsRepository = new ClientsRepository(clientsInMemoryStorage, _clientEntityToClientMapper);
        }

        /// <summary>
        /// Тест для проверки успешного создания записи о клиенте.
        /// </summary>
        [Fact(DisplayName = "CreateClient. Запись о клиенте создана.")]
        public async void CreateClient_ValidClientEntity_CreatedClientId()
        {
            // Arrange

            // Act
            long clientId = await _clientsRepository.CreateClient(_client, _cancellationToken);

            // Assert
            Assert.Equal(_client.Id, clientId);
        }
        
        /// <summary>
        /// Тест для проверки успешного поиска клиента по его id.
        /// </summary>
        [Fact(DisplayName = "GetClientById. Найден клиент по его id.")]
        public async void GetClientById_ValidClientId_CorrectClientInfo()
        {
            // Arrange

            // Act
            Client clientById = await _clientsRepository.GetClientById(_client.Id, _cancellationToken);

            // Assert
            Assert.Equivalent(_client, clientById);
        }

        /// <summary>
        /// Тест для проверки успешного обновления записи о клиенте.
        /// </summary>
        [Fact(DisplayName = "UpdateClient. Запись о клиенте обновлена.")]
        public async void UpdateClient_ValidClientEntity_CreatedClient()
        {
            // Arrange

            // Act
            Client updateClient = await _clientsRepository.UpdateClient(_client.Id, _client, _cancellationToken);

            // Assert
            Assert.Equivalent(_client, updateClient);
        }
        
        /// <summary>
        /// Тест для проверки успешного поиска клиентов по фильтру.
        /// </summary>
        [Theory(DisplayName = "GetClientsByFilter. Найден клиент по фильтру.")]
        [ClassData(typeof(BuildClientFilters))]
        public async void GetClientsByFilter_ValidClientFilter_CorrectClientInfo(ClientFilter clientFilter)
        {
            // Arrange

            // Act
            IReadOnlyCollection<Client> clientsByFilter = await _clientsRepository.GetClientsByFilter(clientFilter, _cancellationToken);

            // Assert
            Assert.Equivalent(_clients, clientsByFilter);
        }
    }
}
