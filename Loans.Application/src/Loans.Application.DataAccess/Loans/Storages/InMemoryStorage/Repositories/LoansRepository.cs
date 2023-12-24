using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Loans.Repositories;
using Loans.Application.DataAccess.Models;
using Loans.Application.DataAccess.Storages.InMemoryStorage;

namespace Loans.Application.DataAccess.Loans.Storages.InMemoryStorage.Repositories;

/// <inheritdoc/>
internal class LoansRepository : ILoansRepository
{
    private readonly IInMemoryStorage<Loan, LoanEntity> _loansInMemoryStorage;

    private readonly IMapper<LoanEntity, Loan> _loanEntityToLoanMapper;
    
    public LoansRepository(IInMemoryStorage<Loan, LoanEntity> loansInMemoryStorage,
        IMapper<LoanEntity, Loan> loanEntityToLoanMapper)
    {
        _loansInMemoryStorage = loansInMemoryStorage ?? throw new ArgumentNullException(nameof(loansInMemoryStorage));
        _loanEntityToLoanMapper = loanEntityToLoanMapper ?? throw new ArgumentNullException(nameof(loanEntityToLoanMapper));
    }
    
    /// <inheritdoc/>
    public async Task<long> CreateLoan(Loan loan, CancellationToken cancellationToken)
    {
        await _loansInMemoryStorage.Add(loan, cancellationToken);

        return _loansInMemoryStorage.Values.Last().Id;
    }
    
    /// <inheritdoc/>
    public Task<Loan> GetLoanById(long loanId, CancellationToken cancellationToken)
    {
        Loan loan = _loansInMemoryStorage.Values
            .Where(loanInfo => loanInfo.Id == loanId)
            .Select(loanInfo => _loanEntityToLoanMapper.Map(loanInfo))
            .Single();

        return Task.FromResult(loan);
    }

    /// <inheritdoc/>
    public async Task<Loan> UpdateLoan(long loanId, Loan loan, CancellationToken cancellationToken)
    {
        await _loansInMemoryStorage.Update(loanId, loan, cancellationToken);
        Loan loanById = await GetLoanById(loanId, cancellationToken);

        return loanById;
    }
    
    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<Loan>> GetLoansByClientId(long clientId, CancellationToken cancellationToken)
    {
        List<Loan> loans = _loansInMemoryStorage.Values
            .Where(loanInfo => loanInfo.ClientId == clientId)
            .Select(loanInfo => _loanEntityToLoanMapper.Map(loanInfo))
            .ToList();
        
        return await Task.FromResult(loans.AsReadOnly());
    }
}