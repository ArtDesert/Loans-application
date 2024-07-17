using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.AppServices.Clients.Handlers;
using Loans.Application.DataAccess.Providers;
using Loans.Application.DataAccess.Repositories;
using Loans.Application.UnitTests.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog.Core;
using Xunit;

namespace Loans.Application.UnitTests.ClientTests;

public class FilterClientsHandlerTests
{
	private readonly DbContextOptions<LoansApplicationContext> _dbContextOptions;

	public FilterClientsHandlerTests()
	{
		_dbContextOptions = new DbContextOptionsBuilder<LoansApplicationContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;
	}

	internal LoansApplicationContext InitializeDataContext()
	{
		var context = new LoansApplicationContext(_dbContextOptions);
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();
		context.ChangeTracker.Clear();
		return context;
	}

	[Fact]
	internal async Task HandleAsync_FilterClientsAsync_ReturnedTwoClients()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var repository = new ClientRepository(context);
		var handler = new FilterClientsHandler(context, null);
		await repository.FillDatabaseWithTestData();
		var template = new ClientFilter()
		{
			LastName = "Д"
		};
		var expectedClients = new List<long>() { 2, 3 };

		//Act
		var clients = await handler.HandleAsync(template);
		var actualClients = clients.ToList();

		//Assert
		Assert.Equal(2, actualClients.Count());
		for (int i = 0; i < expectedClients.Count; ++i)
		{
			Assert.Equal(expectedClients[i], actualClients[i].Id);
		}
	}

	[Fact]
	internal async Task HandleAsync_FilterClientsAsync_ReturnedAllClients()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var repository = new ClientRepository(context);
		var handler = new FilterClientsHandler(context, null);
		await repository.FillDatabaseWithTestData();
		var template = new ClientFilter();
		var expectedClients = new List<long>() { 1, 2, 3 };

		//Act
		var clients = await handler.HandleAsync(template);
		var actualClients = clients.ToList();

		//Assert
		Assert.Equal(3, actualClients.Count());
		for (int i = 0; i < expectedClients.Count; ++i)
		{
			Assert.Equal(expectedClients[i], actualClients[i].Id);
		}
	}

	[Fact]
	internal async Task HandleAsync_FilterClientsAsync_ReturnedOneClients()
	{
		//Arrange
		LoansApplicationContext context = InitializeDataContext();
		var repository = new ClientRepository(context);
		var handler = new FilterClientsHandler(context, null);
		await repository.FillDatabaseWithTestData();
		var template = new ClientFilter()
		{
			LastName = "Фадеева"
		};
		var expectedClients = new List<long>() { 1 };

		//Act
		var clients = await handler.HandleAsync(template);
		var actualClients = clients.ToList();

		//Assert
		Assert.Single(actualClients);
		for (int i = 0; i < expectedClients.Count; ++i)
		{
			Assert.Equal(expectedClients[i], actualClients[i].Id);
		}
	}
}
