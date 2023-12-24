using Loans.Application.Api.Contracts.Clients.Controllers;
using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.Api.Contracts.Clients.Responses;
using Loans.Application.Api.Contracts.Dto;
using Loans.Application.AppServices.Contracts.Clients.Exceptions;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Loans.Application.AppServices.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Loans.Application.Host.Clients.Controllers;

/// <inheritdoc cref = "IClientsController"/>
[ApiController]
[Route("client")]
public class ClientsController : ControllerBase, IClientsController
{
    private readonly IClientRequestHandlers _clientRequestHandlers;
    private readonly ILoanRequestHandlers _loanRequestHandlers ;

    /// <summary>
    /// Инициализирует новый экземпляр класса ClientController/>.
    /// </summary>
    /// <param name="clientRequestHandlers">Обработчики запросов по клиентам.</param>
    /// <param name="loanRequestHandlers">Обработчики запросов по кредитам.</param>
    public ClientsController(IClientRequestHandlers clientRequestHandlers, ILoanRequestHandlers loanRequestHandlers)
    {
        _clientRequestHandlers = clientRequestHandlers ?? throw new ArgumentNullException(nameof(clientRequestHandlers));
        _loanRequestHandlers = loanRequestHandlers ?? throw new ArgumentNullException(nameof(loanRequestHandlers));
    }

    /// <inheritdoc/>
    [HttpGet("{clientId:long}/loans")]
    public async Task<GetLoansByClientIdResponse> GetLoansByClientId([FromRoute] long clientId,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<LoanDto> loansIdByClientId = await
            _loanRequestHandlers.GetLoansByClientId(clientId, cancellationToken);
        GetLoansByClientIdResponse getLoansByClientIdResponse = new(loansIdByClientId.ToArray());
        
        return getLoansByClientIdResponse;
    }
    
    /// <inheritdoc/>
    [HttpPost("get-clients-by-filter")]
    public async Task<GetClientsByFilterResponse> GetClientsByFilter(
        [FromBody] GetClientByFilterRequest getClientByFilterRequest, CancellationToken cancellationToken)
    {
        ClientFilter clientFilter = new(getClientByFilterRequest.FirstName, getClientByFilterRequest.MiddleName,
            getClientByFilterRequest.LastName, getClientByFilterRequest.BirthDate);
        IReadOnlyCollection<ClientDto> clientsByFilter = await
            _clientRequestHandlers.GetClientsByFilter(clientFilter, cancellationToken);
            
        return new GetClientsByFilterResponse(clientsByFilter.ToArray());
    }
    
    /// <inheritdoc/>
    [HttpPost("create-client")]
    public async Task<CreateClientResponse> CreateClient(
        [FromBody] CreateClientRequest createClientRequest, CancellationToken cancellationToken)
    {
        long clientId = await _clientRequestHandlers.CreateClient(createClientRequest.FirstName, 
            createClientRequest.MiddleName, createClientRequest.LastName, createClientRequest.BirthDate,
            createClientRequest.Salary, cancellationToken);
        CreateClientResponse createClientResponse = new(clientId);

        return createClientResponse;
    }
}