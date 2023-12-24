using DCS.DecisionMakerService.Client.Api.Queries;
using DCS.DecisionMakerService.Client.Api.Responses;
using Loans.Application.Api.Contracts.Dto;
using Loans.Application.Api.Contracts.Enums;
using Loans.Application.AppServices.Clients.Repositories;
using Loans.Application.AppServices.Contracts.HttpClients.DecisionMakerService;
using Loans.Application.AppServices.Contracts.Loans.Exceptions;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Validators;
using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Loans.Handlers;
using Loans.Application.AppServices.Loans.Repositories;
using Loans.Application.UnitTests.Data.Clients;
using Loans.Application.UnitTests.Data.DecisionMakerService;
using Loans.Application.UnitTests.Data.Loans;
using NSubstitute;
using Xunit;

namespace Loans.Application.UnitTests.Tests.Loans.Application.AppServices.Loans.Handlers
{
    /// <summary>
    /// Тестовый класс для проверки функциональности обработчиков запросов кредитов.
    /// </summary>
    public class LoanRequestHandlersTests
    {
        private readonly ILoanRequestHandlers _loanRequestHandlers;
        private readonly ILoanValidator _loanValidator;
        private readonly CancellationToken _cancellationToken;
        private readonly ILoansRepository _loansRepository;
        private readonly IClientsRepository _clientsRepository;
        private readonly IDecisionMakerService _decisionMakerService;
        private readonly Loan _loan;
        private readonly LoanDto _loanDto;
        private readonly List<Loan> _loans;
        private readonly List<LoanDto> _loanDtos;
        private readonly Client _client;
        private readonly ClientDto _clientDto;
        private readonly IMapper<Client, ClientDto> _clientToClientDtoMapper;
        private readonly IMapper<LoanDto, Loan> _loanDtoToLoanMapper;
        private readonly IMapper<Loan, LoanDto> _loanToLoanDtoMapper;
        
        /// <summary>
        /// Инициализация объектов перед каждым тестом.
        /// </summary>
        public LoanRequestHandlersTests()
        {
            _cancellationToken = new();
            
            BuildLoan buildLoan = new();
            _loan = buildLoan.GetLoan();
            _loans = new()
            {
                _loan
            };
            _loanDto = buildLoan.GetLoanDto();
            _loanDtos = new()
            {
                _loanDto
            };
            
            BuildClient buildClient = new();
            _clientDto = buildClient.GetClientDto();
            _client = buildClient.GetClient();
            
            _decisionMakerService = Substitute.For<IDecisionMakerService>();
            
            _loanValidator = Substitute.For<ILoanValidator>();
            
            _loansRepository = Substitute.For<ILoansRepository>();
            
            _clientsRepository = Substitute.For<IClientsRepository>();
            
            _clientToClientDtoMapper = Substitute.For<IMapper<Client, ClientDto>>();
            
            _loanDtoToLoanMapper = Substitute.For<IMapper<LoanDto, Loan>>();
            
            _loanToLoanDtoMapper = Substitute.For<IMapper<Loan, LoanDto>>();
            
            _loanRequestHandlers = new LoanRequestHandlers(_loanValidator, _loansRepository, _clientsRepository,
                _decisionMakerService, _clientToClientDtoMapper, _loanDtoToLoanMapper, _loanToLoanDtoMapper);
        }

        /// <summary>
        /// Тест для проверки успешного создания кредита и получения его Id.
        /// </summary>
        [Fact(DisplayName = "CreateLoan. Кредит создан. Выдан его Id.")]
        public async void CreateLoan_ValidLoanData_LoanId()
        {
            // Arrange
            _clientsRepository.GetClientById(Arg.Any<long>(), Arg.Any<CancellationToken>()).Returns(_client);
            _clientToClientDtoMapper.Map(Arg.Any<Client>()).Returns(_clientDto);
            _loanValidator.Validate(Arg.Any<LoanDto>()).Returns(new List<string>());
            _loanDtoToLoanMapper.Map(Arg.Any<LoanDto>()).Returns(_loan);
            _loansRepository.CreateLoan(Arg.Any<Loan>(), _cancellationToken).Returns(_loanDto.Id);
            BuildCalculateDecisionResponse buildCalculateDecisionResponse = new BuildCalculateDecisionResponse();
            CalculateDecisionResponse calculateDecisionResponse =
                buildCalculateDecisionResponse.GetApprovedCalculateDecisionResponse();
            _decisionMakerService.CalculateDecision(Arg.Any<CalculateDecisionQuery>(), _cancellationToken).Returns(calculateDecisionResponse);
            _loansRepository.UpdateLoan(Arg.Any<long>(), Arg.Any<Loan>(), Arg.Any<CancellationToken>()).Returns(_loan);
            
            //Act
            long loanId = await _loanRequestHandlers.CreateLoan(_loanDto.Client.Id, _loanDto.Amount,
                _loanDto.LoanTermInMonths, _cancellationToken);
            
            // Assert
            Assert.Equal(_loan.Id, loanId);
        }

        /// <summary>
        /// Тест для проверки выброса исключения при ошибке валидации данных кредита.
        /// </summary>
        [Theory(DisplayName = "CreateLoan. Исключение. Ошибка валидации данных договора.")]
        [ClassData(typeof(BuildInvalidLoans))]
        public async void CreateLoan_InvalidData_LoanValidationException(LoanDto loanDto, List<string> validationErrors)
        {
            // Arrange
            _clientsRepository.GetClientById(Arg.Any<long>(), Arg.Any<CancellationToken>()).Returns(_client);
            _clientToClientDtoMapper.Map(Arg.Any<Client>()).Returns(_clientDto);
            _loanValidator.Validate(Arg.Any<LoanDto>()).Returns(validationErrors);
            
            // Act
            Exception? exception = await Record.ExceptionAsync(() => _loanRequestHandlers.CreateLoan(loanDto.Client.Id,
                loanDto.Amount, loanDto.LoanTermInMonths, _cancellationToken));
            
            // Assert
            Assert.IsType<LoanValidationException>(exception);
            Assert.Equivalent( "Ошибка валидации данных кредитного договора. " + validationErrors, exception.Message);
        }

        /// <summary>
        /// Проверка корректного вывода статуса кредита.
        /// </summary>
        [Fact(DisplayName = "CheckLoanStatus. Выдан статус кредита.")]
        public async void CheckLoanStatus_ValidData_LoanStatus()
        {
            // Arrange
            _loansRepository.GetLoanById(Arg.Any<long>(), Arg.Any<CancellationToken>()).Returns(_loan);
            
            // Act
            LoanStatus loanStatus = await _loanRequestHandlers.CheckLoanStatus(_loan.Id, _cancellationToken);

            // Assert
            Assert.Equivalent(_loan.Status, loanStatus);
        }
        
        /// <summary>
        /// Проверка поиска кредитного контракта по id.
        /// </summary>
        [Fact(DisplayName = "GetLoanById. Найден кредитный контракт по его id.")]
        public async void GetLoanById_ValidData_Loan()
        {
            // Arrange
            _loansRepository.GetLoanById(Arg.Any<long>(), Arg.Any<CancellationToken>()).Returns(_loan);
            _clientsRepository.GetClientById(Arg.Any<long>(), Arg.Any<CancellationToken>()).Returns(_client);
            _loanToLoanDtoMapper.Map(Arg.Any<Loan>()).Returns(_loanDto);
            _clientToClientDtoMapper.Map(Arg.Any<Client>()).Returns(_clientDto);
            
            // Act
            LoanDto loanDtoById = await _loanRequestHandlers.GetLoanById(_loan.Id, _cancellationToken);

            // Assert
            Assert.Equivalent(_loanDto, loanDtoById);
        }
        
        /// <summary>
        /// Проверка поиска кредитного контракта по id клиента.
        /// </summary>
        [Fact(DisplayName = "GetLoansByClientId. Найден кредитный контракт по id клиента.")]
        public async void GetLoansByClientId_ValidData_Loan()
        {
            // Arrange
            _loansRepository.GetLoansByClientId(Arg.Any<long>(), Arg.Any<CancellationToken>())
                .Returns(_loans.AsReadOnly());
            _clientsRepository.GetClientById(Arg.Any<long>(), Arg.Any<CancellationToken>()).Returns(_client);
            _clientToClientDtoMapper.Map(Arg.Any<Client>()).Returns(_clientDto);
            _loanToLoanDtoMapper.Map(Arg.Any<Loan>()).Returns(_loanDto);

            // Act
            IReadOnlyCollection<LoanDto> loanDtosById = await _loanRequestHandlers.GetLoansByClientId(_loanDto.Client.Id, _cancellationToken);

            // Assert
            Assert.Equivalent(_loanDtos, loanDtosById);
        }
    }
}