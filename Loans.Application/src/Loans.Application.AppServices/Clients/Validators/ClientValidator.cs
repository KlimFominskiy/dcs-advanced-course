using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Clients.Validators;
using Loans.Application.AppServices.Contracts.Models;

namespace Loans.Application.AppServices.Clients.Validators;

/// <inheritdoc/>
internal class ClientValidator : IClientValidator
{
    private readonly ClientSpecification _clientSpecification;
    
    public ClientValidator(ClientSpecification clientSpecification)
    {
        _clientSpecification = clientSpecification ?? throw new ArgumentNullException(nameof(clientSpecification));
    }

    /// <inheritdoc/>
    public List<string> Validate(ClientDto clientDto)
    {
        List<string> validationErrors = new();
        
        DateTime minDateOfBirth = new(
            DateTime.Now.Year - _clientSpecification.MinAgeInYears,
            DateTime.Now.Month,
            DateTime.Now.Day);

        DateTime maxDateOfBirth = new(
            DateTime.Now.Year - _clientSpecification.MaxAgeInYears,
            DateTime.Now.Month,
            DateTime.Now.Day);
        
        if (clientDto.BirthDate > minDateOfBirth)
        {
            validationErrors.Add(
                $"Минимальный возраст клиента - {_clientSpecification.MinAgeInYears}.");
        }

        if (clientDto.BirthDate < maxDateOfBirth)
        {
            validationErrors.Add(
            $"Максимальный возраст клиента - {_clientSpecification.MaxAgeInYears}.");
        }
        
        if (string.IsNullOrWhiteSpace(clientDto.FirstName))
        {
            validationErrors.Add("Имя клиента обязательно для заполнения.");
        }
        
        if (string.IsNullOrWhiteSpace(clientDto.LastName))
        {
            validationErrors.Add("Фамилия клиента обязательна для заполнения.");
        }

        if (clientDto.Salary <= 0)
        {
            validationErrors.Add("Месячный доход клиента должен быть больше нуля.");
        }
        
        return validationErrors;
    }
}