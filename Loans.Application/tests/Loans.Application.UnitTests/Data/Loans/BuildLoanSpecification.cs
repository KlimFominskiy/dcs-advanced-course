using Loans.Application.AppServices.Contracts.Models;

namespace Loans.Application.UnitTests.Data.Loans
{
    /// <summary>
    /// Класс для предоставления данных по спецификации кредитных договоров в контексте тестирования.
    /// </summary>
    public class BuildLoanSpecification
    {
        /// <summary>
        /// Получает объект специфиикации кредитного договора с заданными параметрами.
        /// </summary>
        /// <returns>Объект специфиикации кредитного договора с заданными параметрами.</returns>
        public LoanSpecification GetLoanSpecification()
        {
            LoanSpecification loanSpecification = new(
                MinLoanAmount: 1000,
                MaxLoanAmount: 50000,
                MinLoanTermInYears: 1,
                MaxLoanTermInYears: 10,
                MinMonthlyIncome: 1000);

            return loanSpecification;
        }
    }
}