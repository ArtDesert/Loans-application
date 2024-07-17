using DbUp.Engine;
using DbUp;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Loans.Migrations;

public class Program
{
	public static void Main(string[] args)
	{
		IConfiguration configuration = new ConfigurationBuilder()
			.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

		var connection = configuration.GetConnectionString("loans-application-service-db-connection");
		UpgradeEngine builder = DeployChanges.To.
			PostgresqlDatabase(connection)
			.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
			.LogToConsole()
			.Build();

		DatabaseUpgradeResult result = builder.PerformUpgrade();

		if (!result.Successful)
		{
			throw new Exception("Migrations could not be applied to the database", result.Error);
		}
		else
		{
			Console.WriteLine("LoansApplicationService DB Migrations complete, app is ready to start.");
		}
	}
}