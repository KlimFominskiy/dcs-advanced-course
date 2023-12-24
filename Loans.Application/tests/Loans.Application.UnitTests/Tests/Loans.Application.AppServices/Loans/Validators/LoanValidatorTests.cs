using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Loans.Validators;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Loans.Validators;
using Loans.Application.UnitTests.Data.Clients;
using Loans.Application.UnitTests.Data.Loans;
using Xunit;

namespace Loans.Application.UnitTests.Tests.Loans.Application.AppServices.Loans.Validators
{
    /// <summary>
    /// Тестовый класс для проверки функциональности валидатора кредитных договоров.
    /// </summary>
    public class LoanValidatorTests
    {
        private readonly ILoanValidator _loanValidator;
        private readonly LoanDto _loanDto;

        /// <summary>
        /// Инициализация объектов перед каждым тестом.
        /// </summary>
        public LoanValidatorTests()
        {
            BuildLoan buildLoan = new();
            _loanDto = buildLoan.GetLoanDto();
            
            BuildLoanSpecification buildLoanSpecification = new();
            BuildClientSpecification buildClientSpecification = new();
            LoanSpecification loanSpecification = buildLoanSpecification.GetLoanSpecification();
            ClientSpecification clientSpecification = buildClientSpecification.GetClientSpecification();
            _loanValidator = new LoanValidator(loanSpecification, clientSpecification);
        }

        /// <summary>
        /// Тест для проверки успешной валидации кредитного договора.
        /// </summary>
        [Fact(DisplayName = "Validate. Валидация кредитного договора успешна")]
        public void Validate_ValidLoanData_NoValidationErrors()
        {
            // Arrange

            // Act
            List<string> validationErrors = _loanValidator.Validate(_loanDto);
            
            // Assert
            Assert.Empty(validationErrors);
        }

        /// <summary>
        /// Тест для проверки выброса исключения при ошибке валидации данных кредитного договора.
        /// </summary>
        [Theory(DisplayName = "Validate. Исключение. Ошибка валидации данных кредитного договора.")]
        [ClassData(typeof(BuildInvalidLoans))]
        public void Validate_InvalidClientData_ClientValidationErrors(LoanDto loanDto, List<string> validationErrors)
        {
            // Arrange

            // Act
            List<string> actualValidationErrors = _loanValidator.Validate(loanDto);
            
            // Assert
            Assert.Equivalent(validationErrors, actualValidationErrors);
        }
    }
}