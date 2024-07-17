using Loans.Application.Api.Contracts.Loans.Enums;
using Loans.Application.AppServices.Contracts.Loans.Entities;
using Loans.Application.DataAccess.Repositories;

namespace Loans.Application.UnitTests.Extensions;

internal static class LoanRepositoryExtensions
{
	internal static async Task<List<long>> FillDatabaseWithTestData(this LoanRepository repository)
	{
		var loansIds = new List<long>();
		var loans = new List<Loan>
		{
			new()
			{
				ClientId = 1,
				Amount = 50000000,
				PeriodInMonth = 50,
				InterestRate = 0.1M,
				CreationDate = DateTime.Now,
				DenialReason = "Неизвестная причина",
				Status = LoanStatus.Denied
			},
			new()
			{
				ClientId = 1,
				Amount = 20000,
				PeriodInMonth = 24,
				InterestRate = 1.7M,
				CreationDate = DateTime.Now,
				Status = LoanStatus.Approved
			},
			new()
			{
				ClientId = 2,
				Amount = 500000,
				PeriodInMonth = 36,
				InterestRate = 0.6M,
				CreationDate = DateTime.Now,
				Status = LoanStatus.InProgress
			},          
			new()
			{
				ClientId = 2,
				Amount = 200000,
				PeriodInMonth = 48,
				InterestRate = 2.3M,
				CreationDate = DateTime.Now,
				Status = LoanStatus.InProgress
			},          
			new()
			{
				ClientId = 3,
				Amount = 50000000,
				PeriodInMonth = 100,
				InterestRate = 2.5M,
				CreationDate = DateTime.Now,
				Status = LoanStatus.Unknown
			}
		};
		foreach (var loan in loans)
		{
			long currentId = await repository.CreateAsync(loan);
			loansIds.Add(currentId);
		}
		return loansIds;
	}
}
