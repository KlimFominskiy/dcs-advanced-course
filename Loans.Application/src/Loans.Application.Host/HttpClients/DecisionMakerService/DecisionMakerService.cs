using DCS.DecisionMakerService.Client.Api.Queries;
using DCS.DecisionMakerService.Client.Api.Responses;
using Loans.Application.AppServices.Contracts.HttpClients.DecisionMakerService;

namespace Loans.Application.Host.HttpClients.DecisionMakerService;

/// <inheritdoc/>
internal class DecisionMakerService : IDecisionMakerService
{
    private readonly HttpClient _httpClient;
    private readonly DecisionMakerOptions _decisionMakerOptions;

    public DecisionMakerService(HttpClient httpClient, DecisionMakerOptions decisionMakerOptions)
    {
        _httpClient = httpClient  ?? throw new ArgumentNullException(nameof(httpClient));
        _decisionMakerOptions = decisionMakerOptions ?? throw new ArgumentNullException(nameof(decisionMakerOptions));
        httpClient.BaseAddress = new Uri(_decisionMakerOptions.BaseDecisionMakerUrl);
    }

    /// <inheritdoc/>
    public async Task<CalculateDecisionResponse> CalculateDecision(CalculateDecisionQuery calculateDecisionQuery,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage httpResponseMessage =
            await _httpClient.PostAsJsonAsync(_decisionMakerOptions.DecisionMakerCalculateDecisionUrl,
                    calculateDecisionQuery, cancellationToken);
        CalculateDecisionResponse calculateDecisionResponse =
            await httpResponseMessage.Content.ReadFromJsonAsync<CalculateDecisionResponse>(cancellationToken:
                cancellationToken) ?? throw new InvalidOperationException("Пустой ответ от сервиса принятий решений");
        
        return calculateDecisionResponse;
    }
}