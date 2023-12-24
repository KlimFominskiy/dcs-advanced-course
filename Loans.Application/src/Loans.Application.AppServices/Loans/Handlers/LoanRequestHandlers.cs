using System.Reflection;
using DCS.DecisionMakerService.Client.Api.Enums;
using DCS.DecisionMakerService.Client.Api.Queries;
using DCS.DecisionMakerService.Client.Api.Responses;
using Loans.Application.Api.Contracts.Dto;
using Loans.Application.Api.Contracts.Enums;
using Loans.Application.AppServices.Clients.Repositories;
using Loans.Application.AppServices.Contracts;
using Loans.Application.AppServices.Contracts.HttpClients.DecisionMakerService;
using Loans.Application.AppServices.Contracts.Loans.Exceptions;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Validators;
using Loans.Application.AppServices.Contracts.Mappers;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Loans.Repositories;
using Loans.Application.AppServices.Mappers;

namespace Loans.Application.AppServices.Loans.Handlers;

/// <inheritdoc/>
internal class LoanRequestHandlers : ILoanRequestHandlers
{
    private readonly ILoanValidator _loanValidator;
    
    private readonly ILoansRepository _loansRepository;
    
    private readonly IClientsRepository _clientsRepository;
    
    private readonly IDecisionMakerService _decisionMakerService;
    
    private readonly IMapper<Client, ClientDto> _clientToClientDtoMapper;
    
    private readonly IMapper<LoanDto, Loan> _loanDtoToLoanMapper;
    
    private readonly IMapper<Loan, LoanDto> _loanToLoanDtoMapper;
    
    
    public LoanRequestHandlers(ILoanValidator loanValidator, ILoansRepository loansRepository,
        IClientsRepository clientsRepository, IDecisionMakerService decisionMakerService,
        IMapper<Client, ClientDto> clientToClientDtoMapper, IMapper<LoanDto, Loan> loanDtoToLoanMapper,
        IMapper<Loan, LoanDto> loanToLoanDtoMapper)
    {
        _loanValidator = loanValidator ?? throw new ArgumentNullException(nameof(loanValidator));
        _loansRepository = loansRepository ?? throw new ArgumentNullException(nameof(loansRepository));
        _clientsRepository = clientsRepository ?? throw new ArgumentNullException(nameof(clientsRepository));
        _decisionMakerService = decisionMakerService ?? throw new ArgumentNullException(nameof(decisionMakerService));
        _clientToClientDtoMapper = clientToClientDtoMapper ?? throw new ArgumentNullException(nameof(clientToClientDtoMapper));
        _loanDtoToLoanMapper = loanDtoToLoanMapper ?? throw new ArgumentNullException(nameof(loanDtoToLoanMapper));
        _loanToLoanDtoMapper = loanToLoanDtoMapper ?? throw new ArgumentNullException(nameof(loanToLoanDtoMapper));
    }
    
    /// <inheritdoc/>
    public async Task<long> CreateLoan(long clientId, decimal amount, int loanTermInMonths, 
        CancellationToken cancellationToken)
    {
        Client client = await _clientsRepository.GetClientById(clientId, cancellationToken);
        ClientDto clientDto = _clientToClientDtoMapper.Map(client);
        LoanDto loanDto = new()
        {
            Id = default,
            Client = clientDto,
            Amount = amount,
            LoanTermInMonths = loanTermInMonths,
            ExpectedInterestRate = default,
            CreationDate = DateTime.Now,
            Status = default,
            RejectionReason = "",
            AnnuityAmount = default
        };
        List<string> validationErrors = _loanValidator.Validate(loanDto);
        if (validationErrors.Count > 0)
        {
            throw new LoanValidationException(validationErrors);
        }

        Loan loan = _loanDtoToLoanMapper.Map(loanDto);
        PropertyInfo property = typeof(Loan).GetProperty("ClientId") ?? throw new InvalidOperationException("У модели нет свойства ClientId");
        property.SetValue(loan, client.Id);
        
        long loanId = await _loansRepository.CreateLoan(loan, cancellationToken);
        
        CalculateDecisionQuery calculateDecisionQuery = new()
        {
            ApplicationId = loanId,
            ApplicationDate = loanDto.CreationDate,
            CreditAmount = loanDto.Amount,
            CreditLenMonth = loanDto.LoanTermInMonths,
            ClientId = loanDto.Client.Id,
            BirthDay = loanDto.Client.BirthDate,
            IncomeAmount = loanDto.Client.Salary
        };
        CalculateDecisionResponse calculateDecisionResponse = await _decisionMakerService
            .CalculateDecision(calculateDecisionQuery, cancellationToken);
        LoanStatus loanStatus;
        switch (calculateDecisionResponse.Decision.DecisionStatus)
        {
            case DecisionStatus.Approval:
                loanStatus = LoanStatus.Approved;
                loan = ApproveLoan(calculateDecisionResponse, calculateDecisionQuery.ApplicationDate);
                break;
            case DecisionStatus.Refuse:
                loanStatus = LoanStatus.Denied;
                break;
            case DecisionStatus.Underwriting:
                loanStatus = LoanStatus.InProgress;
                break;
            default:
                throw new Exception($"Не удалось оформить кредит. Статус кредитного договора - {LoanStatus.Unknown}");
        }
        if (loanStatus != LoanStatus.Approved)
        {
            loan = DenyLoan(calculateDecisionQuery, loanStatus);
        }
        
        Loan updatedLoan = await _loansRepository.UpdateLoan(loanId, loan, cancellationToken);
        
        return updatedLoan.Id;
    }

    /// <inheritdoc/>
    public async Task<LoanStatus> CheckLoanStatus(long loanId, CancellationToken cancellationToken)
    {
        Loan loanByLoanId = await _loansRepository.GetLoanById(loanId, cancellationToken);

        return loanByLoanId.Status;
    }

    /// <inheritdoc/>
    public async Task<LoanDto> GetLoanById(long loanId, CancellationToken cancellationToken)
    {
        Loan loan = await _loansRepository.GetLoanById(loanId, cancellationToken);
        Client client = await _clientsRepository.GetClientById(loan.ClientId, cancellationToken);

        LoanDto loanDto = _loanToLoanDtoMapper.Map(loan);
        PropertyInfo property = typeof(LoanDto).GetProperty("Client")
                                ?? throw new InvalidOperationException("У класса нет свойства Client");
        property.SetValue(loanDto, _clientToClientDtoMapper.Map(client));
            
        return loanDto;
    }
    
    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<LoanDto>> GetLoansByClientId(long clientId, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<Loan> loansByClientId = await _loansRepository.GetLoansByClientId(clientId, cancellationToken);
        Client client = await _clientsRepository.GetClientById(clientId, cancellationToken);
        ClientDto clientDto = _clientToClientDtoMapper.Map(client);
        
        IReadOnlyCollection<LoanDto> loansByClientIdDto = loansByClientId
            .Select(loanInfo =>
            {
                LoanDto loanDto = _loanToLoanDtoMapper.Map(loanInfo);
                PropertyInfo property = typeof(LoanDto).GetProperty("Client")
                                        ?? throw new InvalidOperationException("У класса нет свойства Client");
                property.SetValue(loanDto, clientDto);
                
                return loanDto;
            })
            .ToList()
            .AsReadOnly();
        
        return loansByClientIdDto;
    }

    private Loan ApproveLoan(CalculateDecisionResponse calculateDecisionResponse, DateTime creationDate)
    {
        Loan loan = new()
        {
            Id = calculateDecisionResponse.ApplicationId,
            ClientId = calculateDecisionResponse.ClientId,
            Amount = calculateDecisionResponse.Decision.LoanOffer.CreditAmount,
            LoanTermInMonths = calculateDecisionResponse.Decision.LoanOffer.CreditLenMonth,
            ExpectedInterestRate = calculateDecisionResponse.Decision.LoanOffer.InterestRate,
            CreationDate = creationDate,
            Status = LoanStatus.Approved,
            RejectionReason = "",
            AnnuityAmount = calculateDecisionResponse.Decision.LoanOffer.AnnuityAmount
        };

        return loan;
    }

    private Loan DenyLoan(CalculateDecisionQuery calculateDecisionQuery, LoanStatus loanStatus)
    {
        Loan loan = new()
        {
            Id = calculateDecisionQuery.ApplicationId,
            ClientId = calculateDecisionQuery.ClientId,
            Amount = calculateDecisionQuery.CreditAmount,
            LoanTermInMonths = calculateDecisionQuery.CreditLenMonth,
            ExpectedInterestRate = default,
            CreationDate = calculateDecisionQuery.ApplicationDate,
            Status = loanStatus,
            RejectionReason = "",
            AnnuityAmount = default
        };

        return loan;
    }
}