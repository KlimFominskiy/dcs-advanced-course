using System.Collections;
using Loans.Application.AppServices.Contracts.Models;

namespace Loans.Application.UnitTests.Data.Clients
{
    /// <summary>
    /// Класс для предоставления набора данных фильтров клиентов в контексте тестирования.
    /// </summary>
    public class BuildClientFilters : IEnumerable<object[]>
    {
        /// <summary>
        /// Получает коллекцию фильтров клиентов.
        /// </summary>
        /// <returns>Коллекция фильтров клиентов.</returns>
        public List<ClientFilter> GetClientFilters()
        {
            ClientFilter filterByFirstName = new(
                FirstName: "Александр",
                MiddleName: "",
                LastName: "",
                BirthDate: null
            );
            ClientFilter filterByMiddleName = new(
                FirstName: "",
                MiddleName: "Сергеевич",
                LastName: "",
                BirthDate: null
            );
            ClientFilter filterByLastName = new(
                FirstName: "",
                MiddleName: "",
                LastName: "Пушкин",
                BirthDate: null
            );
            ClientFilter filterByDateOfBirth = new(
                FirstName: "",
                MiddleName: "",
                LastName: "",
                BirthDate: DateTime.Parse("2000-01-01")
            );

            List<ClientFilter> clientFilters = new()
            {
                filterByFirstName,
                filterByMiddleName,
                filterByLastName,
                filterByDateOfBirth
            };
            
            return clientFilters;
        }
        
        /// <summary>
        /// Возвращает перечислитель для коллекции объектов.
        /// </summary>
        /// <returns>Перечислитель для коллекции объектов.</returns>
        public IEnumerator<object[]> GetEnumerator()
        {
            List<ClientFilter> clientFilters = GetClientFilters();
            foreach (ClientFilter clientFilter in clientFilters)
            {
                yield return new object[] { clientFilter };
            }
        }
        
        /// <summary>
        /// Возвращает перечислитель для коллекции объектов.
        /// </summary>
        /// <returns>Перечислитель для коллекции объектов.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}