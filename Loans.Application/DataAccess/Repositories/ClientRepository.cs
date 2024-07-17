using Loans.Application.AppServices.Contracts.Clients.Entities;
using Loans.Application.AppServices.Contracts.Clients.Repositories;
using Loans.Application.DataAccess.Providers;
using Microsoft.EntityFrameworkCore;

namespace Loans.Application.DataAccess.Repositories;

internal class ClientRepository : IClientRepository
{
    private readonly LoansApplicationContext _context;

    public ClientRepository(LoansApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<long> CreateAsync(Client entity, CancellationToken token = default)
    {
		await _context.Clients.AddAsync(entity, token);
        await _context.SaveChangesAsync(token);
        return entity.Id;
    }

    public async Task<Client?> GetByIdAsync(long id, CancellationToken token = default)
	{
        return await _context.Clients.FirstOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task DeleteAsync(long id, CancellationToken token = default)
    {
		_context.Clients.Remove(new Client() { Id = id });
		await _context.SaveChangesAsync(token);
    }

	public async Task UpdateAsync(Client updatedClient, CancellationToken token = default)
	{
        _context.Clients.Update(updatedClient);
        await _context.SaveChangesAsync(token);
	}
}
