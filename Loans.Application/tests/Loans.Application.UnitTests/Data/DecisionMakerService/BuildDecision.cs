using DCS.DecisionMakerService.Client.Api.Enums;
using DCS.DecisionMakerService.Client.Api.Models;

namespace Loans.Application.UnitTests.Data.DecisionMakerService
{
    /// <summary>
    /// Класс для создания объекта решения в контексте тестирования.
    /// </summary>
    public class BuildDecision
    {
        /// <summary>
        /// Получает объект решения с подтвержденным статусом и предложением по кредиту.
        /// </summary>
        /// <returns>Объект решения.</returns>
        public Decision GetApprovedDecision()
        {
            BuildLoanOffer buildLoanOffer = new();
            Decision decision = new()
            {
                DecisionStatus = DecisionStatus.Approval,
                LoanOffer = buildLoanOffer.GetLoanOffer()
            };

            return decision;
        }
    }
}