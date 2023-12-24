using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Validators;

namespace Loans.Application.AppServices.Contracts.Clients.Validators;

/// <summary>
/// Валидатор модели данных клиента.
/// </summary>
public interface IClientValidator : IValidator<ClientDto>
{
    
}