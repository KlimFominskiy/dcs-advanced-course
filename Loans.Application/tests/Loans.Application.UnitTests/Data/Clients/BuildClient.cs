using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Models;
using Loans.Application.DataAccess.Models;

namespace Loans.Application.UnitTests.Data.Clients
{
    /// <summary>
    /// Класс для создания объектов клиента и связанных с ним данных в контексте тестирования.
    /// </summary>
    public class BuildClient
    {
        /// <summary>
        /// Создает и возвращает объект клиента.
        /// </summary>
        /// <returns>Объект клиента.</returns>
        public Client GetClient()
        {
            Client client = new()
            {
                Id = 1,
                FirstName = "Александр",
                MiddleName = "Сергеевич",
                LastName = "Пушкин",
                BirthDate = new DateTime(2000, 01, 01),
                Salary = 2500
            };

            return client;
        }

        /// <summary>
        /// Создает и возвращает объект DTO клиента.
        /// </summary>
        /// <returns>Объект DTO клиента.</returns>
        public ClientDto GetClientDto()
        {
            Client client = GetClient();
            ClientDto clientDto = new()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                BirthDate = client.BirthDate,
                Salary = client.Salary
            };

            return clientDto;
        }
        
        /// <summary>
        /// Создает и возвращает объект сущность клиента.
        /// </summary>
        /// <returns>Объект сущность клиента.</returns>
        public ClientEntity GetClientEntity()
        {
            Client client = GetClient();
            ClientEntity clientEntity = new()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                BirthDate = client.BirthDate,
                Salary = client.Salary
            };

            return clientEntity;
        }
    }
}