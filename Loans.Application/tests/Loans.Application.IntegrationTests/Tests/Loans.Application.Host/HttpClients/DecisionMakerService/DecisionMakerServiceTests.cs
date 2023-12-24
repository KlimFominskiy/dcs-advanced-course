using DCS.DecisionMakerService.Client.Api.Enums;
using DCS.DecisionMakerService.Client.Api.Queries;
using DCS.DecisionMakerService.Client.Api.Responses;
using Loans.Application.AppServices.Contracts.HttpClients.DecisionMakerService;
using Loans.Application.Host.HttpClients.DecisionMakerService;
using Loans.Application.IntegrationTests.Data.DecisionMakerService;
using Xunit;

namespace Loans.Application.IntegrationTests.Tests.Loans.Application.AppServices.HttpClients.DecisionMakerService
{
    /// <summary>
    /// Тесты для взаимодействия с сервисом принятия решений.
    /// </summary>
    public class DecisionMakerServiceTests
    {
        private readonly IDecisionMakerService _decisionMakerService;
        private readonly CancellationToken _cancellationToken;
        private readonly BuildCalculateDecisionQuery _buildCalculateDecisionQuery;
        
        /// <summary>
        /// Инициализация объектов перед каждым тестом.
        /// </summary>
        public DecisionMakerServiceTests()
        {
            _buildCalculateDecisionQuery = new();
            _cancellationToken = new CancellationToken();
            BuildDecisionMakerOptions buildDecisionMakerOptions = new();
            HttpClient httpClient = new HttpClient();
            DecisionMakerOptions decisionMakerOptions = buildDecisionMakerOptions.GetDecisionMakerOptions();
            _decisionMakerService = new Host.HttpClients.DecisionMakerService.DecisionMakerService(httpClient, decisionMakerOptions);
        }
        
        /// <summary>
        /// Тест на проверку отказа при малой сумме кредита.
        /// </summary>
        [Fact(DisplayName = "CalculateDecision. Пришёл отказ из сервиса решений.")]
        public async void CalculateDecision_SmallCreditAmount_DeniedStatus()
        {
            // Arrange
            CalculateDecisionQuery calculateDecisionQuery = _buildCalculateDecisionQuery.GetDeniedCalculateDecisionQuery();
            
            // Act
            CalculateDecisionResponse calculateDecisionResponse = await
                _decisionMakerService.CalculateDecision(calculateDecisionQuery, _cancellationToken);
            
            // Assert
            Assert.Equal(DecisionStatus.Refuse, calculateDecisionResponse.Decision.DecisionStatus);
        }
        
        /// <summary>
        /// Тест на проверку одобрения при наличии одного продукта.
        /// </summary>
        [Fact(DisplayName = "CalculateDecision. Пришло одобрение из сервиса решений.")]
        public async void CalculateDecision_OneProductIsAvailableQuery_ApprovedStatus()
        {
            // Arrange
            CalculateDecisionQuery calculateDecisionQuery = _buildCalculateDecisionQuery.GetApprovedCalculateDecisionQuery();
            
            // Act
            CalculateDecisionResponse calculateDecisionResponse = await
                _decisionMakerService.CalculateDecision(calculateDecisionQuery, _cancellationToken);
            
            // Assert
            Assert.Equal(DecisionStatus.Approval, calculateDecisionResponse.Decision.DecisionStatus);
        }
        
        /// <summary>
        /// Тест на проверку ручной проверки заявки при наличии нескольких продуктов.
        /// </summary>
        [Fact(DisplayName = "CalculateDecision. Пришла ручная проверка заявки.")]
        public async void CalculateDecision_MultipleProductsAreAvailableQuery_UnderwritingStatus()
        {
            // Arrange
            CalculateDecisionQuery calculateDecisionQuery = _buildCalculateDecisionQuery.GetUnderwritingCalculateDecisionQuery();

            // Act
            CalculateDecisionResponse calculateDecisionResponse = await
                _decisionMakerService.CalculateDecision(calculateDecisionQuery, _cancellationToken);
            
            // Assert
            Assert.Equal(DecisionStatus.Underwriting, calculateDecisionResponse.Decision.DecisionStatus);
        }
    }
}