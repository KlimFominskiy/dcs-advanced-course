using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Models;

namespace Loans.Application.IntegrationTests.Data.DecisionMakerService;

/// <summary>
/// Класс для создания объектов клиента и связанных с ним данных в контексте тестирования.
/// </summary>
public class BuildClient
{
    /// <summary>
    /// Создает и возвращает объект клиента.
    /// </summary>
    /// <returns>Объект клиента.</returns>
    public Client GetClient()
    {
        Client client = new()
        {
            Id = 1,
            FirstName = "Александр",
            MiddleName = "Сергеевич",
            LastName = "Пушкин",
            BirthDate = new DateTime(2000, 01, 01),
            Salary = 2500
        };

        return client;
    }
}