using DCS.DecisionMakerService.Client.Api.Queries;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Models;

namespace Loans.Application.IntegrationTests.Data.DecisionMakerService
{
    /// <summary>
    /// Строитель запросов на расчет решения от сервиса принятия решений.
    /// </summary>
    public class BuildCalculateDecisionQuery
    {
        /// <summary>
        /// Возвращает запрос, который должен быть одобрен.
        /// </summary>
        /// <returns>Запрос на расчет решения.</returns>
        public CalculateDecisionQuery GetApprovedCalculateDecisionQuery()
        {
            BuildClient buildClient = new BuildClient();
            Client client = buildClient.GetClient();
            CalculateDecisionQuery calculateDecisionQuery = new CalculateDecisionQuery()
            {
                ApplicationId = 1,
                ApplicationDate = DateTime.Now,
                CreditAmount = 10005M,
                CreditLenMonth = 9,
                ClientId = client.Id,
                BirthDay = client.BirthDate,
                IncomeAmount = client.Salary
            };

            return calculateDecisionQuery;
        }
        
        /// <summary>
        /// Возвращает запрос, в котором должно быть отказано.
        /// </summary>
        /// <returns>Запрос на расчет решения.</returns>
        public CalculateDecisionQuery GetDeniedCalculateDecisionQuery()
        {
            BuildClient buildClient = new BuildClient();
            Client client = buildClient.GetClient();
            CalculateDecisionQuery calculateDecisionQuery = new CalculateDecisionQuery()
            {
                ApplicationId = 1,
                ApplicationDate = DateTime.Now,
                CreditAmount = 9M,
                CreditLenMonth = 9,
                ClientId = client.Id,
                BirthDay = client.BirthDate,
                IncomeAmount = client.Salary
            };

            return calculateDecisionQuery;
        }
        
        /// <summary>
        /// Возвращает запрос, который необходимо проверять вручную.
        /// </summary>
        /// <returns>Запрос на расчет решения.</returns>
        public CalculateDecisionQuery GetUnderwritingCalculateDecisionQuery()
        {
            BuildClient buildClient = new BuildClient();
            Client client = buildClient.GetClient();
            CalculateDecisionQuery calculateDecisionQuery = new CalculateDecisionQuery()
            {
                ApplicationId = 1,
                ApplicationDate = DateTime.Now,
                CreditAmount = 1700000M,
                CreditLenMonth = 9,
                ClientId = client.Id,
                BirthDay = client.BirthDate,
                IncomeAmount = client.Salary
            };

            return calculateDecisionQuery;
        }
    }
}