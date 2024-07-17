using Loans.Application.Api.Contracts.Abstractions.Controllers;
using Loans.Application.Api.Contracts.Loans.Enums;
using Loans.Application.Api.Contracts.Loans.Requests;
using Loans.Application.Api.Contracts.Loans.Responses;
using Loans.Application.AppServices.Contracts.Clients.Entities;
using Loans.Application.AppServices.Contracts.Clients.Handlers;
using Loans.Application.AppServices.Contracts.Loans.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Loans.Application.Host.Controllers; 

[ApiController]
[Route("loans")]
public class LoanController : ILoanController
{
	private readonly ICheckLoanStatusHandler _checkLoanStatusHandler;
	private readonly IGetClientByIdHandler _getClientByIdHandler;
	private readonly IGetLoansByClientIdHandler _getLoansByClientIdHandler;
	private readonly IGetLoanByIdHandler _getLoanByIdHandler;
	private readonly IGetLoanIdsByClientIdHandler _getLoanIdsByClientIdHandler;
	private readonly ICreateLoanHandler _createLoanHandler;

	public LoanController(ICheckLoanStatusHandler checkLoanStatusHandler,
						  IGetClientByIdHandler getClientByIdHandler,
						  IGetLoansByClientIdHandler getLoansByClientIdHandler,
						  IGetLoanByIdHandler getLoanByIdHandler,
						  IGetLoanIdsByClientIdHandler getLoanIdsByClientIdHandler,
						  ICreateLoanHandler createLoanHandler)
	{
		_checkLoanStatusHandler = checkLoanStatusHandler ?? throw new ArgumentNullException(nameof(checkLoanStatusHandler));
		_getClientByIdHandler = getClientByIdHandler ?? throw new ArgumentNullException(nameof(getClientByIdHandler));
		_getLoansByClientIdHandler = getLoansByClientIdHandler ?? throw new ArgumentNullException(nameof(getLoansByClientIdHandler));
		_getLoanByIdHandler = getLoanByIdHandler ?? throw new ArgumentNullException(nameof(getLoanByIdHandler));
		_getLoanIdsByClientIdHandler = getLoanIdsByClientIdHandler ?? throw new ArgumentNullException(nameof(getLoanIdsByClientIdHandler));
		_createLoanHandler = createLoanHandler ?? throw new ArgumentNullException(nameof(createLoanHandler));
	}

	[HttpGet("{loanId:long}/status")]
	public async Task<LoanStatus> CheckLoanStatus(long loanId, CancellationToken token)
	{
		return await _checkLoanStatusHandler.HandleAsync(loanId, token);
	}

	[HttpPost("create")]
	public async Task<long> CreateLoanAsync(
		[FromBody] CreateLoanApplicationRequest loanApplication, 
		CancellationToken token)
	{
		Client? applicant = await _getClientByIdHandler.HandleAsync(loanApplication.ClientId, token);
		return await _createLoanHandler.HandleAsync(loanApplication, applicant, token);
	}

	[HttpGet("{clientId:long}/loans")]
	public async Task<ICollection<LoanResponse>> GetLoansByClientIdASync(long clientId, CancellationToken token)
	{
		return await _getLoansByClientIdHandler.HandleAsync(clientId, token);
	}

	[HttpGet("{id:long}")]
	public async Task<LoanResponse> GetLoanByIdAsync(long id, CancellationToken token)
	{
		return await _getLoanByIdHandler.HandleAsync(id, token);
	}

	[HttpGet("{clientId:long}/loan-ids")]
	public async Task<ICollection<long>> GetLoanIdsByClientIdAsync(long clientId, CancellationToken token)
	{
		return await _getLoanIdsByClientIdHandler.HandleAsync(clientId, token);
	}
}
