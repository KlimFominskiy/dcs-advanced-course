using DCS.DecisionMakerService.Client.Api.Models;

namespace Loans.Application.UnitTests.Data.DecisionMakerService
{
    /// <summary>
    /// Класс для создания объекта предложения по кредиту в контексте тестирования.
    /// </summary>
    public class BuildLoanOffer
    {
        /// <summary>
        /// Получает объект предложения по кредиту с заданными параметрами.
        /// </summary>
        /// <returns>Объект предложения по кредиту.</returns>
        public LoanOffer GetLoanOffer()
        {
            LoanOffer loanOffer = new LoanOffer()
            {
                CreditAmount = 20_000,
                CreditLenMonth = 12,
                InterestRate = 0.2000M,
                AnnuityAmount = 2_000
            };

            return loanOffer;
        }
    }
}