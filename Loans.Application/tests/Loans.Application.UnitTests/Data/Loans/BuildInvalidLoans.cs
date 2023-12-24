using System.Collections;
using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.UnitTests.Data.Clients;

namespace Loans.Application.UnitTests.Data.Loans;

/// <summary>
/// Класс для предоставления недопустимых данных по кредитным договорам в контексте тестирования.
/// </summary>
public class BuildInvalidLoans : IEnumerable<object[]>
{
    /// <summary>
    /// Получает список недопустимых данных по кредитным договорам.
    /// </summary>
    /// <returns>Список объектов <see cref="LoanDto"/> с недопустимыми данными.</returns>
    public static List<LoanDto> GetInvalidLoansDtos()
    {
        LoanDto loanWithoutClientFirstName = new()
        {
            Client = new()
            {
                FirstName = "",
                LastName = "Пушкин",
                BirthDate = DateTime.Parse("2000-01-02"),
                Salary = 2500
            },
            Amount = 1250,
            LoanTermInMonths = 14
        };
        
        LoanDto loanWithoutClientLastName = new()
        {
            Client = new()
            {
                FirstName = "Александр",
                LastName = "",
                BirthDate = DateTime.Parse("2000-01-02"),
                Salary = 2500
            },
            Amount = 1250,
            LoanTermInMonths = 14
        };
        
        LoanDto loanWithTooYoungClient = new()
        {
            Client = new()
            {
                FirstName = "Александр",
                LastName = "Пушкин",
                BirthDate = DateTime.Parse("2020-01-02"),
                Salary = 2500
            },
            Amount = 1250,
            LoanTermInMonths = 14
        };
        
        LoanDto loanWithTooOldClient = new()
        {
            Client = new()
            {
                FirstName = "Александр",
                LastName = "Пушкин",
                BirthDate = DateTime.Parse("1900-01-02"),
                Salary = 2500
            },
            Amount = 1250,
            LoanTermInMonths = 14
        };
        
        LoanDto loanWithTooSmallClientSalary = new()
        {
            Client = new()
            {
                FirstName = "Александр",
                LastName = "Пушкин",
                BirthDate = DateTime.Parse("2000-01-02"),
                Salary = 250
            },
            Amount = 1250,
            LoanTermInMonths = 14
        };
        
        LoanDto loanWithTooBigLoanAmount = new()
        {
            Client = new()
            {
                FirstName = "Александр",
                LastName = "Пушкин",
                BirthDate = DateTime.Parse("2000-01-02"),
                Salary = 2500
            },
            Amount = 55_000,
            LoanTermInMonths = 14
        };
        
        LoanDto loanWithTooSmallLoanAmount = new()
        {
            Client = new()
            {
                FirstName = "Александр",
                LastName = "Пушкин",
                BirthDate = DateTime.Parse("2000-01-02"),
                Salary = 2500
            },
            Amount = 500,
            LoanTermInMonths = 14
        };
        
        LoanDto loanWithTooSmallLoanTerm = new()
        {
            Client = new()
            {
                FirstName = "Александр",
                LastName = "Пушкин",
                BirthDate = DateTime.Parse("2000-01-02"),
                Salary = 2500
            },
            Amount = 1250,
            LoanTermInMonths = 8
        };
        
        LoanDto loanWithTooBigLoanTerm = new()
        {
            Client = new()
            {
                FirstName = "Александр",
                LastName = "Пушкин",
                BirthDate = DateTime.Parse("2000-01-02"),
                Salary = 2500
            },
            Amount = 1250,
            LoanTermInMonths = 125
        };

        List<LoanDto> loans = new()
        {
            loanWithoutClientFirstName,
            loanWithoutClientLastName,
            loanWithTooYoungClient,
            loanWithTooOldClient,
            loanWithTooSmallClientSalary,
            loanWithTooBigLoanAmount,
            loanWithTooSmallLoanAmount,
            loanWithTooSmallLoanTerm,
            loanWithTooBigLoanTerm,
        };

        return loans;
    }

    /// <summary>
    /// Создаёт коллекцию ошибок валидации данных кредитного договора.
    /// </summary>
    /// <returns>Коллекцию ошибок валидации кредитного договора.</returns>
    public List<List<string>> GetValidationErrors()
    {
        BuildClientSpecification buildClientSpecification = new BuildClientSpecification();
        ClientSpecification clientSpecification = buildClientSpecification.GetClientSpecification();
        BuildLoanSpecification buildLoanSpecification = new BuildLoanSpecification();
        LoanSpecification loanSpecification = buildLoanSpecification.GetLoanSpecification();
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
                $"Месячный доход клиента не может быть меньше {loanSpecification.MinMonthlyIncome}."
            },
            new List<string>
            {
                $"Сумма кредита не может быть больше {loanSpecification.MaxLoanAmount}."
            },
            new List<string>
            {
                $"Сумма кредита не может быть меньше {loanSpecification.MinLoanAmount}."
            },
            new List<string>
            {
                $"Срок кредита в годах не может быть меньше {loanSpecification.MinLoanTermInYears}."
            },
            new List<string>
            {
                $"Срок кредита не может быть больше {loanSpecification.MaxLoanTermInYears} лет."
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
        List<LoanDto> loanDtos = GetInvalidLoansDtos();
        List<List<string>> validationErrors = GetValidationErrors();
        for (int i = 0; i < loanDtos.Count; i++)
        {
            yield return new object[] { loanDtos[i], validationErrors[i]};
        }
    }

    /// <summary>
    /// Возвращает перечислитель для коллекции объектов.
    /// </summary>
    /// <returns>Перечислитель для коллекции объектов.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}