using System.Collections;
using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Models;

namespace Loans.Application.UnitTests.Data.Clients
{
    /// <summary>
    /// Класс для предоставления набора невалидных данных клиентов в контексте тестирования.
    /// </summary>
    public class BuildInvalidClients : IEnumerable<object[]>
    {
        /// <summary>
        /// Получает коллекцию объектов данных клиентов с неверными значениями.
        /// </summary>
        /// <returns>Коллекция объектов данных клиентов.</returns>
        public List<ClientDto> GetInvalidClientsDtos()
        {
            ClientDto clientDtoWithoutFirstName = new()
            {
                Id = 1,
                FirstName = "",
                MiddleName = "Петрович",
                LastName = "Авенариус",
                BirthDate = DateTime.Parse("1990-01-02"),
                Salary = 10_000
            };
            ClientDto clientDtoWithoutLastName = new()
            {
                Id = 1,
                FirstName = "Михаил",
                MiddleName = "Петрович",
                LastName = "",
                BirthDate = DateTime.Parse("1990-01-02"),
                Salary = 10_000
            };
            ClientDto tooYoungClient = new()
            {
                Id = 1,
                FirstName = "Михаил",
                MiddleName = "Петрович",
                LastName = "Авенариус",
                BirthDate = DateTime.Parse("2020-01-02"),
                Salary = 10_000
            };
            ClientDto tooOldClient = new()
            {
                Id = 1,
                FirstName = "Михаил",
                MiddleName = "Петрович",
                LastName = "Авенариус",
                BirthDate = DateTime.Parse("1900-01-02"),
                Salary = 10_000
            };
            ClientDto clientDtoWithNegativeSalary = new()
            {
                Id = 1,
                FirstName = "Михаил",
                MiddleName = "Петрович",
                LastName = "Авенариус",
                BirthDate = DateTime.Parse("1990-01-02"),
                Salary = -100
            };
            List<ClientDto> invalidClientsDtos = new List<ClientDto>()
            {
                clientDtoWithoutFirstName,
                clientDtoWithoutLastName,
                tooYoungClient,
                tooOldClient,
                clientDtoWithNegativeSalary
            };

            return invalidClientsDtos;
        }

        /// <summary>
        /// Создаёт коллекцию ошибок валидации данных клиента.
        /// </summary>
        /// <returns>Коллекцию ошибок валидации клиента.</returns>
        public List<List<string>> GetValidationErrors()
        {
            BuildClientSpecification buildClientSpecification = new BuildClientSpecification();
            ClientSpecification clientSpecification = buildClientSpecification.GetClientSpecification();
            List<List<string>> validationErrors = new()
            {
                new List<string>
                {
                    "Имя клиента обязательно для заполнения."
                },
                new List<string>
                {
                    "Фамилия клиента обязательна для заполнения."
                },
                new List<string>
                {
                    $"Минимальный возраст клиента - {clientSpecification.MinAgeInYears}."
                },
                new List<string>
                {
                    $"Максимальный возраст клиента - {clientSpecification.MaxAgeInYears}."
                },
                new List<string>
                {
                    "Месячный доход клиента должен быть больше нуля."
                }
            };

            return validationErrors;
        }

        /// <summary>
        /// Возвращает перечислитель для коллекции объектов.
        /// </summary>
        /// <returns>Перечислитель для коллекции объектов.</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            List<ClientDto> clientDtos = GetInvalidClientsDtos();
            List<List<string>> validationErrors = GetValidationErrors();
            for (int i = 0; i < clientDtos.Count; i++)
            {
                yield return new object[] { clientDtos[i], validationErrors[i] };
            }
        }
    
        /// <summary>
        /// Возвращает перечислитель для коллекции объектов.
        /// </summary>
        /// <returns>Перечислитель для коллекции объектов.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}