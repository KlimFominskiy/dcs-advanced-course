using Loans.Application.Host.HttpClients.DecisionMakerService;

namespace Loans.Application.IntegrationTests.Data.DecisionMakerService
{
    /// <summary>
    /// Опции для клиента сервиса принятия решений.
    /// </summary>
    public class BuildDecisionMakerOptions
    {
        /// <summary>
        /// Возвращает опции для клиента сервиса принятия решений.
        /// </summary>
        /// <returns>Опции для клиента сервиса принятия решений.</returns>
        public DecisionMakerOptions GetDecisionMakerOptions()
        {
            DecisionMakerOptions decisionMakerOptions = new DecisionMakerOptions(
                "http://localhost:2001",
                "/DecisionMaker/calculate-decision");

            return decisionMakerOptions;
        }
    }
}