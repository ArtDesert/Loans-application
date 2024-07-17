using Loans.Application.Api.Contracts.Loans.Enums;
using Loans.Application.AppServices.Contracts.Loans.Entities;
using Loans.Application.DataAccess.Providers;
using Loans.Application.DataAccess.Repositories;
using Loans.Application.UnitTests.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Loans.Application.UnitTests.LoanTests;

public class LoanRepositoryTests
{
	private readonly DbContextOptions<LoansApplicationContext> _dbContextOptions;

	public LoanRepositoryTests()
	{
		_dbContextOptions = new DbContextOptionsBuilder<LoansApplicationContext>()
			.UseInMemoryDatabase(databaseName: "dcs")
			.Options;
	}

	internal LoansApplicationContext InitializeDataContext()
	{
		var context = new LoansApplicationContext(new DbContextOptionsBuilder<LoansApplicationContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options);
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();
		return context;
	}

	//CreateAsync,GetByIdAsync,UpdatAsync,DeleteAsync,GetLoansByClientIdAsync

	[Fact]
	internal async Task CreateAsync_AddingNewLoanstIntoDb_DbContainsNewEntries()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var clientRepository = new ClientRepository(context);
		var loanRepository = new LoanRepository(context);
		await clientRepository.FillDatabaseWithTestData();

		//Act
		await loanRepository.FillDatabaseWithTestData();

		//Assert
		Assert.Equal(5, await context.Loans.CountAsync());
	}


	[Fact]
	internal async Task GetByIdAsync_GettingExistingLoan_ReturnedCorrectEntry()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var clientRepository = new ClientRepository(context);
		var loanRepository = new LoanRepository(context);
		await clientRepository.FillDatabaseWithTestData();
		var loansIds = await loanRepository.FillDatabaseWithTestData();

		//Act
		long id = loansIds[0];
		var receivedLoan = await loanRepository.GetByIdAsync(id);

		//Assert
		Assert.Equal(id, receivedLoan.Id);
		Assert.Equal(1, receivedLoan.ClientId);
		Assert.Equal(LoanStatus.Denied, receivedLoan.Status);
		Assert.Equal(50, receivedLoan.PeriodInMonth);
	}

	[Fact]
	internal async Task UpdateAsync_UpdatingLoan_UpdatingIsWorkCorrect()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var clientRepository = new ClientRepository(context);
		var loanRepository = new LoanRepository(context);
		await clientRepository.FillDatabaseWithTestData();
		var loansIds = await loanRepository.FillDatabaseWithTestData();
		foreach (var entity in context.ChangeTracker.Entries())
		{
			entity.State = EntityState.Detached;
		}
		var updatedLoan = new Loan()
		{
			Id = loansIds[1],
			ClientId = 3,
			Amount = 10000,
			PeriodInMonth = 24,
			InterestRate = 1.7M,
			CreationDate = DateTime.Now,
			Status = LoanStatus.Denied
		};

		//Act
		await loanRepository.UpdateAsync(updatedLoan);
		var actualLoan = await context.Loans.FirstOrDefaultAsync(x => x.Id == loansIds[1]);

		//Assert
		Assert.Equal(5, await context.Loans.CountAsync());
		Assert.Equal(3, actualLoan.ClientId);
		Assert.Equal(10000, actualLoan.Amount);
		Assert.Equal(LoanStatus.Denied, actualLoan.Status);
	}

	[Fact]
	internal async Task GetLoansByClientIdAsync_GettingLoansByClientId2_ReturnedTwoEntries()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var clientRepository = new ClientRepository(context);
		var loanRepository = new LoanRepository(context);
		await clientRepository.FillDatabaseWithTestData();
		var loansIds = await loanRepository.FillDatabaseWithTestData();
		foreach (var entity in context.ChangeTracker.Entries())
		{
			entity.State = EntityState.Detached;
		}
		int clientId = 2;
		var expectedLoansIds = new List<long>() { loansIds[2], loansIds[3] };

		//Act
		var loans = await loanRepository.GetLoansByClientIdAsync(clientId);
		var actualLoans = loans.ToList();

		//Assert
		Assert.Equal(2, actualLoans.Count);
		for (int i = 0; i < expectedLoansIds.Count; ++i)
		{
			Assert.Equal(expectedLoansIds[i], actualLoans[i].Id);
		}
	}

	[Fact]
	internal async Task DeleteAsync_DeletingExistingLoanFromDb_DbNotContainsDeletedEntryAndClientsCountIsEqual4()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var clientRepository = new ClientRepository(context);
		var loanRepository = new LoanRepository(context);
		await clientRepository.FillDatabaseWithTestData();
		var loansIds = await loanRepository.FillDatabaseWithTestData();
		long id = loansIds[2];
		foreach (var entity in context.ChangeTracker.Entries())
		{
			entity.State = EntityState.Detached;
		}

		//Act
		await loanRepository.DeleteAsync(id);

		//Assert
		Assert.Equal(4, await context.Loans.CountAsync());
		Assert.Null(await context.Loans.FirstOrDefaultAsync(x => x.Id == id));
	}
}
