using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Clients.Validators;
using Loans.Application.AppServices.Contracts.Clients.Validators;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.UnitTests.Data.Clients;
using Xunit;

namespace Loans.Application.UnitTests.Tests.Loans.Application.AppServices.Clients.Validators
{
    /// <summary>
    /// Тестовый класс для проверки функциональности валидатора клиентов.
    /// </summary>
    public class ClientValidatorTests
    {
        private readonly IClientValidator _clientValidator;
        private readonly ClientDto _clientDto;

        /// <summary>
        /// Инициализация объектов перед каждым тестом.
        /// </summary>
        public ClientValidatorTests()
        {
            BuildClient buildClient = new();
            _clientDto = buildClient.GetClientDto();
            
            BuildClientSpecification buildClientSpecification = new();
            ClientSpecification clientSpecification = buildClientSpecification.GetClientSpecification();
            
            _clientValidator = new ClientValidator(clientSpecification);
        }

        /// <summary>
        /// Тест для проверки успешной валидации клиента.
        /// </summary>
        [Fact(DisplayName = "Validate. Валидация клиента успешна.")]
        public void Validate_ValidClientData_NoValidationErrors()
        {
            // Arrange

            // Act
            List<string> validationErrors = _clientValidator.Validate(_clientDto);
            
            // Assert
            Assert.Empty(validationErrors);
        }

        /// <summary>
        /// Тест для проверки выброса исключения при ошибке валидации данных клиента.
        /// </summary>
        [Theory(DisplayName = "Validate. Исключение. Ошибка валидации данных клиента.")]
        [ClassData(typeof(BuildInvalidClients))]
        public void Validate_InvalidClientData_ClientValidationErrors(ClientDto clientDto, List<string> validationErrors)
        {
            // Arrange

            // Act
            List<string> actualValidationErrors = _clientValidator.Validate(clientDto);
            
            // Assert
            Assert.Equivalent(validationErrors, actualValidationErrors);
        }
    }
}