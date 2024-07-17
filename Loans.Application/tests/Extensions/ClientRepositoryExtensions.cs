using Loans.Application.AppServices.Contracts.Clients.Entities;
using Loans.Application.DataAccess.Repositories;

namespace Loans.Application.UnitTests.Extensions;

internal static class ClientRepositoryExtensions
{
	internal static async Task<List<long>> FillDatabaseWithTestData(this ClientRepository repository)
	{
		var clientsIds = new List<long>();
		var clients = new List<Client>
		{
			new()
			{
				LastName = "Фадеева",
				FirstName = "Дарья",
				MiddleName = "Витальевна",
				BirthDate = new DateTime(2004, 05, 18),
				Salary = 6000
			},
			new()
			{
				LastName = "Дробин",
				FirstName = "Роман",
				MiddleName = "Равильевич",
				BirthDate = new DateTime(2002, 01, 18),
				Salary = 7000
			},
			new()
			{
				LastName = "Дубровский",
				FirstName = "Никита",
				MiddleName = "Артемович",
				BirthDate = new DateTime(2001, 05, 11),
				Salary = 5000
			}
		};
		foreach (var client in clients)
		{
			long currentId = await repository.CreateAsync(client);
			clientsIds.Add(currentId);
		}
		return clientsIds;
	}
}
