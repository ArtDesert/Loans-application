using Loans.Application.AppServices.Contracts.Clients.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Application.DataAccess.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
	public void Configure(EntityTypeBuilder<Client> builder)
	{
		builder.ToTable(name: "clients", schema: "dcs_loans");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.IsRequired()
			.HasColumnName("id");

		builder.Property(x => x.LastName)
			.HasMaxLength(50)
			.IsUnicode()
			.IsRequired()
			.HasColumnName("last_name");

		builder.Property(x => x.FirstName)
			.HasMaxLength(50)
			.IsUnicode()
			.IsRequired()
			.HasColumnName("first_name");

		builder.Property(x => x.MiddleName)
			.HasMaxLength(50)
			.IsUnicode()
			.HasColumnName("middle_name");

		builder.Property(x => x.BirthDate)
			.IsRequired()
			.HasColumnName("birth_date");

		builder.Property(x => x.Salary)
			.IsRequired()
			.HasColumnName("salary");

		builder.HasMany(c => c.Loans)
			.WithOne(l => l.Client)
			.HasForeignKey(l => l.ClientId);
	}
}
