namespace Loans.Application.AppServices.Models
{
    /// <summary>
    /// Детали об ошибке, используемые для формирования JSON-ответа.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Текстовое сообщение о типе ошибки.
        /// </summary>
        public required string Error { get; init; }

        /// <summary>
        /// Дополнительные детали об ошибке.
        /// </summary>
        public required List<string> Details { get; init; }
    }
}