using Loans.Application.Api.Contracts.Dto;
using Loans.Application.Api.Contracts.Enums;
using Loans.Application.Api.Contracts.Loans.Controllers;
using Loans.Application.Api.Contracts.Loans.Requests;
using Loans.Application.Api.Contracts.Loans.Responses;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Exceptions;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Loans.Application.Host.Loans.Controllers;

/// <inheritdoc cref = "ILoansController"/>
[ApiController]
[Route("loans")]
public class LoansController : ControllerBase, ILoansController
{
    private readonly ILoanRequestHandlers _loanRequestHandlers;

    /// <summary>
    /// Инициализирует новый экземпляр класса LoansController/>.
    /// </summary>
    /// <param name="loanRequestHandlers">Обработчики запросов по кредитам.</param>
    public LoansController(ILoanRequestHandlers loanRequestHandlers)
    {
        _loanRequestHandlers = loanRequestHandlers ?? throw new ArgumentNullException(nameof(loanRequestHandlers));
    }
    
    /// <inheritdoc/>
    [HttpGet("{loanId:long}")]
    public async Task<GetLoanByIdResponse> GetLoanById(long loanId, CancellationToken cancellationToken)
    {
        LoanDto loanDto = await _loanRequestHandlers.GetLoanById(loanId, cancellationToken);
        GetLoanByIdResponse getLoanByIdResponse = new(loanDto);
        
        return getLoanByIdResponse;
    }
    
    /// <inheritdoc/>
    [HttpGet("{loanId:long}/status")]
    public async Task<LoanStatus> CheckLoanStatus(long loanId, CancellationToken cancellationToken)
    {
        LoanStatus loanStatus = await _loanRequestHandlers.CheckLoanStatus(loanId, cancellationToken);
        
        return loanStatus;
    }
    
    /// <inheritdoc/>
    [HttpPost("create-loan")]
    public async Task<CreateLoanResponse> CreateLoan([FromBody] CreateLoanRequest createLoanRequest,
        CancellationToken cancellationToken)
    {
        long loanId = await _loanRequestHandlers.CreateLoan(createLoanRequest.ClientId, createLoanRequest.Amount,
            createLoanRequest.LoanTermInYears * 12, cancellationToken);
        LoanStatus loanStatus = await _loanRequestHandlers.CheckLoanStatus(loanId, cancellationToken);
        CreateLoanResponse createLoanResponse = new CreateLoanResponse(loanId, loanStatus);

        return createLoanResponse;
    }
}