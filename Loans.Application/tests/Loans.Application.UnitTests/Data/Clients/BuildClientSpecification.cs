using Loans.Application.AppServices.Contracts.Models;

namespace Loans.Application.UnitTests.Data.Clients
{
    /// <summary>
    /// Класс для создания объекта спецификации клиента в контексте тестирования.
    /// </summary>
    public class BuildClientSpecification
    {
        /// <summary>
        /// Получает объект спецификации клиента с заданными параметрами.
        /// </summary>
        /// <returns>Объект спецификации клиента.</returns>
        public ClientSpecification GetClientSpecification()
        {
            ClientSpecification clientSpecification = new(
                MaxAgeInYears: 80,
                MinAgeInYears: 12);

            return clientSpecification;
        }
    }
}