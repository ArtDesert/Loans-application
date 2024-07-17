using Loans.Application.Api.Contracts.Loans.Responses;
using Loans.Application.AppServices.Contracts.Loans.Entities;
using Loans.Application.AppServices.Contracts.Loans.Repositories;
using Loans.Application.DataAccess.Providers;
using Microsoft.EntityFrameworkCore;

namespace Loans.Application.DataAccess.Repositories;

internal class LoanRepository : ILoanRepository
{
	private readonly LoansApplicationContext _context;

	public LoanRepository(LoansApplicationContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public async Task<long> CreateAsync(Loan entity, CancellationToken token = default)
	{
		await _context.Loans.AddAsync(entity, token);
		await _context.SaveChangesAsync(token);
		return entity.Id;
	}

	public async Task<Loan?> GetByIdAsync(long id, CancellationToken token = default)
	{
		return await _context.Loans.FirstOrDefaultAsync(x => x.Id == id, token);
	}

	public async Task UpdateAsync(Loan updatedLoan, CancellationToken token = default)
	{
		_context.Loans.Update(updatedLoan);
		await _context.SaveChangesAsync(token);
	}

	public async Task<IEnumerable<LoanResponse>> GetLoansByClientIdAsync(long clientId, CancellationToken token = default)
	{
		return await _context.Loans
			.Where(x => x.ClientId == clientId)
			.Select(loan => new LoanResponse()
			{
				Id = loan.Id,
				ClientId = loan.ClientId,
				Amount = loan.Amount,
				PeriodInMonth = loan.PeriodInMonth,
				InterestRate = loan.InterestRate,
				CreationDate = loan.CreationDate,
				Status = loan.Status
			})
			.ToListAsync(token);
	}

	public async Task DeleteAsync(long id, CancellationToken token = default)
	{
		_context.Remove(new Loan() { Id = id });
		await _context.SaveChangesAsync(token);
	}
}
