using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Loans.Validators;
using Loans.Application.AppServices.Contracts.Models;

namespace Loans.Application.AppServices.Loans.Validators;

/// <inheritdoc/>
internal class LoanValidator : ILoanValidator
{
    private readonly LoanSpecification _loanSpecification;
    private readonly ClientSpecification _clientSpecification;
    
    public LoanValidator(LoanSpecification loanSpecification, ClientSpecification clientSpecification)
    {
        _loanSpecification = loanSpecification ?? throw new ArgumentNullException(nameof(loanSpecification));
        _clientSpecification = clientSpecification ?? throw new ArgumentNullException(nameof(clientSpecification));
    }
    
    /// <inheritdoc/>
    public List<string> Validate(LoanDto loanDto)
    {
        List<string> validationErrors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(loanDto.Client.FirstName))
        {
            validationErrors.Add("Имя клиента обязательно для заполнения.");
        }
        
        if (string.IsNullOrWhiteSpace(loanDto.Client.LastName))
        {
            validationErrors.Add("Фамилия клиента обязательна для заполнения.");
        }
        
        if (DateTime.Now.Year - loanDto.Client.BirthDate.Year < _clientSpecification.MinAgeInYears)
        {
            validationErrors.Add($"Минимальный возраст клиента - {_clientSpecification.MinAgeInYears}.");
        }
        
        if (DateTime.Now.Year - loanDto.Client.BirthDate.Year > _clientSpecification.MaxAgeInYears)
        {
            validationErrors.Add($"Максимальный возраст клиента - {_clientSpecification.MaxAgeInYears}.");
        }

        if (loanDto.Client.Salary < _loanSpecification.MinMonthlyIncome)
        {
            validationErrors.Add($"Месячный доход клиента не может быть меньше {_loanSpecification.MinMonthlyIncome}.");
        }
        
        if (loanDto.Amount < _loanSpecification.MinLoanAmount)
        {
            validationErrors.Add($"Сумма кредита не может быть меньше {_loanSpecification.MinLoanAmount}.");
        }

        if (loanDto.Amount > _loanSpecification.MaxLoanAmount)
        {
            validationErrors.Add($"Сумма кредита не может быть больше {_loanSpecification.MaxLoanAmount}.");
        }

        if (loanDto.LoanTermInMonths < _loanSpecification.MinLoanTermInYears * 12)
        {
            validationErrors.Add($"Срок кредита в годах не может быть меньше {_loanSpecification.MinLoanTermInYears}.");
        }

        if (loanDto.LoanTermInMonths > _loanSpecification.MaxLoanTermInYears * 12)
        {
            validationErrors.Add($"Срок кредита не может быть больше {_loanSpecification.MaxLoanTermInYears} лет.");
        }

        return validationErrors;
    }
}

