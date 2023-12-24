using Loans.Application.Api.Contracts.Dto;
using Loans.Application.Api.Contracts.Enums;
using Loans.Application.AppServices.Contracts.Models;
using Loans.Application.AppServices.Models;
using Loans.Application.DataAccess.Models;
using Loans.Application.UnitTests.Data.Clients;

namespace Loans.Application.UnitTests.Data.Loans
{
    /// <summary>
    /// Класс для предоставления данных по кредитным договорам в контексте тестирования.
    /// </summary>
    public class BuildLoan
    {
        /// <summary>
        /// Получает объект <see cref="Loan"/> с тестовыми данными.
        /// </summary>
        /// <returns>Объект <see cref="Loan"/> с тестовыми данными.</returns>
        public Loan GetLoan()
        {
            BuildClient buildClient = new();
            Loan loan = new()
            {
                Id = 1,
                ClientId = buildClient.GetClient().Id,
                Amount = 20_000,
                LoanTermInMonths = 12,
                ExpectedInterestRate = 0.2000M,
                CreationDate = DateTime.Parse("2023-01-02"),
                Status = LoanStatus.Approved,
                RejectionReason = "",
                AnnuityAmount = 2_000
            };

            return loan;
        }
        
        /// <summary>
        /// Получает объект <see cref="LoanDto"/> с тестовыми данными.
        /// </summary>
        /// <returns>Объект <see cref="LoanDto"/> с тестовыми данными.</returns>
        public LoanDto GetLoanDto()
        {
            Loan loan = GetLoan();
            BuildClient buildClient = new();
            LoanDto loanDto = new()
            {
                Id = loan.Id,
                Client = buildClient.GetClientDto(),
                Amount = loan.Amount,
                LoanTermInMonths = loan.LoanTermInMonths,
                ExpectedInterestRate = loan.ExpectedInterestRate,
                CreationDate = loan.CreationDate,
                Status = loan.Status,
                RejectionReason = loan.RejectionReason,
                AnnuityAmount = loan.AnnuityAmount
            };

            return loanDto;
        }
        
        /// <summary>
        /// Получает объект <see cref="LoanDto"/> с тестовыми данными.
        /// </summary>
        /// <returns>Объект <see cref="LoanDto"/> с тестовыми данными.</returns>
        public LoanEntity GetLoanEntity()
        {
            Loan loan = GetLoan();
            BuildClient buildClient = new();
            LoanEntity loanEntity = new()
            {
                Id = loan.Id,
                ClientId = buildClient.GetClientEntity().Id,
                Amount = loan.Amount,
                LoanTermInMonths = loan.LoanTermInMonths,
                ExpectedInterestRate = loan.ExpectedInterestRate,
                CreationDate = loan.CreationDate,
                Status = loan.Status,
                RejectionReason = loan.RejectionReason,
                AnnuityAmount = loan.AnnuityAmount
            };

            return loanEntity;
        }
    }
}