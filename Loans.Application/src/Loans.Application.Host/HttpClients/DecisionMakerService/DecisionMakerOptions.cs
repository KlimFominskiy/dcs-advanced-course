namespace Loans.Application.Host.HttpClients.DecisionMakerService;

/// <summary>
/// Настройки для сервиса принятий решений.
/// </summary>
public class DecisionMakerOptions
{
    /// <summary>
    /// Инициализация нового экземпляра класса DecisionMakerOptions с указанными параметрами.
    /// </summary>
    /// <param name="baseDecisionMakerUrl">Базовая часть URL сервиса.</param>
    /// <param name="decisionMakerCalculateDecisionUrl">URL выдачи решения по кредиту.</param>
    public DecisionMakerOptions(string baseDecisionMakerUrl, string decisionMakerCalculateDecisionUrl)
    {
        DecisionMakerCalculateDecisionUrl = decisionMakerCalculateDecisionUrl;
        BaseDecisionMakerUrl = baseDecisionMakerUrl;
    }

    /// <summary>
    /// Базовая часть URL сервиса.
    /// </summary>
    public string BaseDecisionMakerUrl { get; init; }
    
    /// <summary>
    /// URL выдачи решения по кредиту.
    /// </summary>
    public string DecisionMakerCalculateDecisionUrl { get; init; }
}