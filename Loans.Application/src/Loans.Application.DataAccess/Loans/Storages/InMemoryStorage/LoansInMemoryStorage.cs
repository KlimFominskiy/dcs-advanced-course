using System.Collections.Concurrent;
using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.DataAccess.Models;
using Loans.Application.DataAccess.Storages.InMemoryStorage;

namespace Loans.Application.DataAccess.Loans.Storages.InMemoryStorage;

/// <inheritdoc/>
internal class LoansInMemoryStorage : InMemoryStorage<Loan, LoanEntity>
{
    public LoansInMemoryStorage(ConcurrentDictionary<long, LoanEntity> entities,
        IMapper<Loan, LoanEntity> loanToLoanEntityMapper) : base(entities, loanToLoanEntityMapper)
    {
    }
}