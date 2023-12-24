using DCS.DecisionMakerService.Client.Api.Responses;
using Loans.Application.UnitTests.Data.Clients;

namespace Loans.Application.UnitTests.Data.DecisionMakerService
{
    /// <summary>
    /// Класс для создания объекта ответа на расчет решения в контексте тестирования.
    /// </summary>
    public class BuildCalculateDecisionResponse
    {
        /// <summary>
        /// Получает объект ответа на расчет решения с подтвержденным решением.
        /// </summary>
        /// <returns>Объект ответа на расчет решения.</returns>
        public CalculateDecisionResponse GetApprovedCalculateDecisionResponse()
        {
            BuildDecision buildDecision = new();
            BuildClient buildClient = new();
            CalculateDecisionResponse calculateDecisionResponse = new(1, buildClient.GetClient().Id,
                buildDecision.GetApprovedDecision());

            return calculateDecisionResponse;
        }
    }
}