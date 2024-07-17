using Loans.Application.AppServices.Contracts.Clients.Entities;
using Loans.Application.DataAccess.Providers;
using Loans.Application.DataAccess.Repositories;
using Loans.Application.UnitTests.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Loans.Application.UnitTests.ClientTests;

public class ClientRepositoryTests
{
    public ClientRepositoryTests()
    {

	}

	internal LoansApplicationContext InitializeDataContext()
	{
		var options = new DbContextOptionsBuilder<LoansApplicationContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;
		var context = new LoansApplicationContext(options);
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();
		context.ChangeTracker.Clear();
		return context;
	}

	[Fact]
    internal async Task CreateAsync_AddingNewClientIntoDb_DbContainsNewEntry()
    {
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var repository = new ClientRepository(context);
		var newCLient = new Client()
        {
            LastName = "Фадеева",
            FirstName = "Дарья",
            MiddleName = "Витальевна",
            BirthDate = new DateTime(2002, 01, 18),
            Salary = 6000
        };

		//Act
		await repository.CreateAsync(newCLient);

        //Assert
        Assert.Equal(1, await context.Clients.CountAsync());
    }

	[Fact]
    internal async Task GetByIdAsync_GettingExistingClientById1_ReturnedEntryWithId1()
    {
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var repository = new ClientRepository(context);
		await repository.FillDatabaseWithTestData();

		//Act
		long id = 1;
		var receivedClient = await repository.GetByIdAsync(id);

		//Assert
		Assert.Equal(id, receivedClient.Id);
	}

	[Fact]
	internal async Task CreateAsync_AddingThreeClients_AutoIncrementOfIdIsWorkCorrect()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var repository = new ClientRepository(context);
		var expectedIdsList = new List<long> { 1, 2, 3 };

		//Act
		var actualIdsList = await repository.FillDatabaseWithTestData();

		//Assert
		for (int i = 0; i < actualIdsList.Count; i++)
		{
			Assert.Equal(expectedIdsList[i], actualIdsList[i]);
		}
		Assert.Equal(3, await context.Clients.CountAsync());
	}

	[Fact]
	internal async Task DeleteAsync_RemovingExistingClientFromDb_DbNotContainsEntryWithId3AndClientsCountIsEqual2()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var repository = new ClientRepository(context);
		await repository.FillDatabaseWithTestData();
		foreach (var entity in context.ChangeTracker.Entries())
		{
			entity.State = EntityState.Detached;
		}

		//Act
		await repository.DeleteAsync(3);

		//Assert
		Assert.Null(await context.Clients.FirstOrDefaultAsync(x => x.Id == 3));
		Assert.Equal(2, await context.Clients.CountAsync());
	}

	[Fact]
	internal async Task UpdateAsync_UpdatingClientWithId1_UpdatingIsWorkCorrect()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var repository = new ClientRepository(context);
		await repository.FillDatabaseWithTestData();
		var updatedClient = new Client()
		{
			Id = 1,
			LastName = "Фадеева",
			FirstName = "Дария",
			MiddleName = "Витальевна",
			BirthDate = new DateTime(2004, 05, 18),
			Salary = 10000
		};
		foreach (var entity in context.ChangeTracker.Entries())
		{
			entity.State = EntityState.Detached;
		}

		//Act
		await repository.UpdateAsync(updatedClient);
		var actualClient = await context.Clients.FirstOrDefaultAsync(x => x.Id == 1);

		//Assert
		Assert.Equal(3, await context.Clients.CountAsync());
		Assert.Equal(10000, actualClient.Salary);
		Assert.Equal("Дария", actualClient.FirstName);
	}
}
