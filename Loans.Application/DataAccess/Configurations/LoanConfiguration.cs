using Loans.Application.AppServices.Contracts.Loans.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loans.Application.DataAccess.Configurations;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
	public void Configure(EntityTypeBuilder<Loan> builder)
	{
		builder.ToTable(name: "loans", schema: "dcs_loans");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.IsRequired()
			.HasColumnName("id");

		builder.Property(x => x.ClientId)
			.IsRequired()
			.HasColumnName("client_id");

		builder.Property(x => x.Amount)
			.IsRequired()
			.HasColumnName("amount");

		builder.Property(x => x.PeriodInMonth)
			.IsRequired()
			.HasColumnName("period_in_month");

		builder.Property(x => x.InterestRate)
			.IsRequired()
			.HasColumnName("interest_rate");

		builder.Property(x => x.CreationDate)
			.IsRequired()
			.HasColumnName("creation_date");

		builder.Property(x => x.Status)
			.IsRequired()
			.HasColumnName("loan_status");

		builder.Property(x => x.DenialReason)
			.HasMaxLength(100)
			.IsUnicode()
			.HasColumnName("denial_reason");

		builder.HasOne(l => l.Client)
			.WithMany(c => c.Loans)
			.HasForeignKey(l => l.ClientId);
	}
}
