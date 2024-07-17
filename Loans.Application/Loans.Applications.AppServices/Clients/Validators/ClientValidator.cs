using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.AppServices.Contracts.Clients.Exceptions;
using Loans.Application.AppServices.Contracts.Clients.Validators;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Loans.Application.AppServices.Clients.Validators;

internal class ClientValidator : IClientValidator
{
	private const int AgeOfMajority = 18;
	private readonly ILogger<ClientValidator> _logger;

    public ClientValidator(ILogger<ClientValidator> logger)
    {
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Validate(CreateClientRequest model, CancellationToken token = default)
	{
		bool isValid = true;
		var validationInfoBuilder = new StringBuilder();
		isValid &= IsConditionValid(string.IsNullOrWhiteSpace(model.FirstName),
									validationInfoBuilder, "first name should not be empty, ");
		isValid &= IsConditionValid(string.IsNullOrWhiteSpace(model.LastName),
									validationInfoBuilder, "first name should not be empty, ");
		isValid &= IsConditionValid(GetAgeByBirthday(model.BirthDate) < AgeOfMajority,
									validationInfoBuilder, "client should be at least 18 years old, ");
		isValid &= IsConditionValid(model.Salary <= 0,
									validationInfoBuilder, "client salary should be > 0, ");
		var validationInfo = validationInfoBuilder.ToString();
		if (!isValid)
		{
			_logger.LogError("Validation failed: {validationInfo}", validationInfo);
			throw new ClientValidationException(validationInfo);
		}
	}

	public bool IsConditionValid(bool condition, StringBuilder validationInfoBuilder, string validationMessage)
	{
		bool result = true;
		if (condition)
		{
			result = false;
			validationInfoBuilder.Append(validationMessage);
		}
		return result;
	}

	public int GetAgeByBirthday(DateTime? birthday)
	{
		var curDate = DateTime.Now;
		int result = curDate.Year - birthday.Value.Year - 1;
		if (birthday.Value.Month < curDate.Month)
		{
			++result;
		}
		else if (birthday.Value.Month == curDate.Month && birthday.Value.Day < curDate.Day)
		{
			++result;
		}
		return result;
	}
}
