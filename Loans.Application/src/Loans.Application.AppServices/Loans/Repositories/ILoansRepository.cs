using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Models;

namespace Loans.Application.AppServices.Loans.Repositories;

/// <summary>
/// Репозиторий для получения из хранилища данных по кредитным договорам.
/// </summary>
public interface ILoansRepository
{
    /// <summary>
    /// Получить запись о кредитном договоре по id кредитого договора.
    /// </summary>
    /// <param name="loanId">Id кредитного догоора.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Данные о кредите.</returns>
    public Task<Loan> GetLoanById(long loanId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить записи о кредитных договорах по id клиента.
    /// </summary>
    /// <param name="clientId">Id клиента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Данные о кредитах клиента.</returns>
    public Task<IReadOnlyCollection<Loan>> GetLoansByClientId(long clientId, CancellationToken cancellationToken);

    /// <summary>
    /// Создать запись о кредитном договоре.
    /// </summary>
    /// <param name="loan">Данные о новом кредитном договоре.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Id созданного кредита.</returns>
    public Task<long> CreateLoan(Loan loan, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновить запись о кредтном договоре.
    /// </summary>
    /// <param name="loanId">Id записи о кредитном договоре.</param>
    /// <param name="loan">Новые данные о кредитном договоре.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновлённая запись о кредитном договоре.</returns>
    public Task<Loan> UpdateLoan(long loanId, Loan loan, CancellationToken cancellationToken);
}