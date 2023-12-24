using Loans.Application.AppServices;
using Loans.Application.AppServices.Contracts;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Loans.Repositories;
using Loans.Application.AppServices.Mappers;
using Loans.Application.AppServices.Models;
using Loans.Application.DataAccess.Clients.Storages.InMemoryStorage;
using Loans.Application.DataAccess.Loans.Storages.InMemoryStorage;
using Loans.Application.DataAccess.Loans.Storages.InMemoryStorage.Repositories;
using Loans.Application.DataAccess.Models;
using Loans.Application.DataAccess.Storages.InMemoryStorage;
using Loans.Application.UnitTests.Data.Clients;
using Loans.Application.UnitTests.Data.Loans;
using NSubstitute;
using Xunit;

namespace Loans.Application.UnitTests.Tests.Loans.Application.DataAccess.Loans.Storages.InMemoryStorage.Repositories
{
    /// <summary>
    /// Тестовый класс для проверки функциональности репозитория кредитных договоров в памяти.
    /// </summary>
    public class LoansRepositoryTests
    {
        private readonly IInMemoryStorage<Loan, LoanEntity> _loansInMemoryStorage;
        private readonly ILoansRepository _loansRepository;
        private readonly CancellationToken _cancellationToken;
        private readonly Loan _loan;
        private readonly LoanEntity _loanEntity;
        private readonly Mapper<LoanEntity, Loan> _loanEntityToLoanMapper;
        private readonly List<LoanEntity> _loanEntities;

        /// <summary>
        /// Инициализация объектов перед каждым тестом.
        /// </summary>
        public LoansRepositoryTests()
        {
            _cancellationToken = new();
            
            BuildClient buildClient = new();
            ClientEntity clientEntity = buildClient.GetClientEntity();
            List<ClientEntity> clientEntities = new()
            {
                clientEntity
            };
            
            BuildLoan buildLoan = new();
            _loan = buildLoan.GetLoan();
            _loanEntity = buildLoan.GetLoanEntity();
            _loanEntities = new()
            {
                _loanEntity
            };
            
            _loansInMemoryStorage = Substitute.For<IInMemoryStorage<Loan, LoanEntity>>();
            _loansInMemoryStorage.Add(Arg.Any<Loan>(), _cancellationToken).Returns(Task.CompletedTask);
            _loansInMemoryStorage.Update(Arg.Any<long>(), Arg.Any<Loan>(), _cancellationToken).Returns(Task.CompletedTask);
            _loansInMemoryStorage.Delete(Arg.Any<long>(), _cancellationToken).Returns(Task.CompletedTask);
            _loansInMemoryStorage.Values.Returns(_loanEntities);
            
            IInMemoryStorage<Client, ClientEntity> clientsInMemoryStorage = Substitute.For<IInMemoryStorage<Client, ClientEntity>>();
            clientsInMemoryStorage.Add(Arg.Any<Client>(), _cancellationToken).Returns(Task.CompletedTask);
            clientsInMemoryStorage.Update(Arg.Any<long>(), Arg.Any<Client>(), _cancellationToken).Returns(Task.CompletedTask);
            clientsInMemoryStorage.Delete(Arg.Any<long>(), _cancellationToken).Returns(Task.CompletedTask);
            clientsInMemoryStorage.Values.Returns(clientEntities);

            _loanEntityToLoanMapper = Substitute.For<Mapper<LoanEntity, Loan>>();
            
            _loansRepository = new LoansRepository(_loansInMemoryStorage, _loanEntityToLoanMapper);
        }
        
        /// <summary>
        /// Тест для проверки успешного создания записи о кредитном договоре.
        /// </summary>
        [Fact(DisplayName = "CreateLoan. Запись о кредитном договоре создана.")]
        public async void CreateLoan_ValidLoan_CreatedLoan()
        {
            //Arrange
            
            // Act
            long loanId = await _loansRepository.CreateLoan(_loan, _cancellationToken);

            // Assert
            Assert.Equal(_loan.Id, loanId);
        }
        
        /// <summary>
        /// Тест для проверки успешного поиска кредитного договора по его id.
        /// </summary>
        [Fact(DisplayName = "GetLoanById. Найден кредитный договор по его id.")]
        public async void GetLoanById_ValidLoanId_CorrectLoan()
        {
            // Arrange

            // Act
            Loan loan = await _loansRepository.GetLoanById(_loan.Id, _cancellationToken);

            // Assert
            Assert.Equivalent(_loan, loan);
        }

        /// <summary>
        /// Тест для проверки успешного обновления записи о кредитном договоре.
        /// </summary>
        [Fact(DisplayName = "UpdateLoan. Запись о кредитном договоре обновлена.")]
        public async void UpdateLoan_ValidLoan_UpdatedLoan()
        {
            // Arrange
            
            // Act
            Loan loan = await _loansRepository.UpdateLoan(_loan.Id, _loan, _cancellationToken);

            // Assert
            Assert.Equivalent(_loan, loan);
        }
        
        /// <summary>
        /// Тест для проверки успешного поиска кредитных договоров по id клиента.
        /// </summary>
        [Fact(DisplayName = "GetLoansByClientId. Найдены кредитные договора по id клиента.")]
        public async void GetLoansByClientId_ValidClientId_CorrectLoanIds()
        {
            // Arrange
            
            // Act
            IReadOnlyCollection<Loan> loansByClientId = await _loansRepository.GetLoansByClientId(1, _cancellationToken);
            
            // Assert
            Assert.Equivalent(_loanEntities, loansByClientId);
        }
    }
}