using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Loans.Application.AppServices.Contracts.Clients.Entities;
using Loans.Application.AppServices.Contracts.Loans.Entities;

namespace Loans.Application.DataAccess.Providers;

public class LoansApplicationContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Loan> Loans { get; set; }

	public LoansApplicationContext(DbContextOptions<LoansApplicationContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
