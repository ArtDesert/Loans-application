using System.Text;
using Microsoft.Extensions.Options;
using Loans.Application.AppServices.Options;
using Loans.Application.AppServices.Clients.Validators;
using Loans.Application.AppServices.Contracts.Clients.Validators;
using Loans.Application.AppServices.Contracts.Loans.Validators;
using Loans.Application.Api.Contracts.Loans.Requests;
using Loans.Application.AppServices.Contracts.Loans.Exceptions;
using Microsoft.Extensions.Logging;

namespace Loans.Application.AppServices.Loans.Validators;

internal class LoanValidator : ILoanValidator
{
	private readonly LoanRestrictionOptions _loanRestriction;
	private readonly IClientValidator _clientValidator;
	private readonly ILogger<LoanValidator> _logger;


	public LoanValidator(IOptionsMonitor<LoanRestrictionOptions> loanRestriction, IClientValidator clientValidator,
						 ILogger<LoanValidator> logger)
	{
		_loanRestriction = loanRestriction?.CurrentValue ?? throw new ArgumentNullException(nameof(loanRestriction));
		_clientValidator = clientValidator ?? throw new ArgumentNullException(nameof(clientValidator));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public void Validate(CreateLoanApplicationRequest loanApp, CancellationToken token = default)
	{
		bool isValid = true;
		var validationInfoBuilder = new StringBuilder();
		var clientValidator = _clientValidator as ClientValidator;
		isValid &= clientValidator.IsConditionValid(loanApp.DesiredAmount < _loanRestriction.MinimumAmount || loanApp.DesiredAmount > _loanRestriction.MaximumAmount,
									validationInfoBuilder, "desired amount does not satisfy rectrictions, ");
		isValid &= clientValidator.IsConditionValid(loanApp.Period < _loanRestriction.MinimumPeriod || loanApp.Period > _loanRestriction.MaximumPeriod,
									validationInfoBuilder, "desired loan period does not satisfy rectrictions, ");
		var validationInfo = validationInfoBuilder.ToString();
		if (!isValid)
		{
			_logger.LogError("Validation failed: {validationInfo}", validationInfo);
			throw new LoanValidationException(validationInfo);
		}
	}
}
