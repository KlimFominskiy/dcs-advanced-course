using DCS.DecisionMakerService.Client.Api.Queries;
using DCS.DecisionMakerService.Client.Api.Responses;

namespace Loans.Application.AppServices.Contracts.HttpClients.DecisionMakerService;

/// <summary>
/// Сервис принятия решений.
/// Осуществляет проверку возможности выдачи кредита.
/// </summary>
public interface IDecisionMakerService
{
    /// <summary>
    /// Скоринг заявки на кредит.
    /// </summary>
    /// <param name="calculateDecisionQuery">Заявки на кредит.</param>
    /// <param name="cancellationToken"> Токен отмены.</param>
    /// <returns>Статус заявки на кредит.</returns>
    public Task<CalculateDecisionResponse> CalculateDecision(CalculateDecisionQuery calculateDecisionQuery, CancellationToken cancellationToken);
}