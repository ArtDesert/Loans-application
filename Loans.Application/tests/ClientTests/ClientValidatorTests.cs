using Loans.Application.Api.Contracts.Clients.Requests;
using Loans.Application.AppServices.Clients.Validators;
using Loans.Application.AppServices.Contracts.Clients.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Loans.Application.UnitTests.ClientTests;

public class ClientValidatorTests
{
    private readonly ClientValidator _validator;
    private readonly IMock<ILogger<ClientValidator>> _loggerMock;

    public ClientValidatorTests()
    {
        _loggerMock = new Mock<ILogger<ClientValidator>>();
        _validator = new ClientValidator(_loggerMock.Object);
    }

    [Fact]
    public void GetAgeByBirthday_ClientWithAgeIs21_Returns21()
    {
        //Arrange
        var birthday = new DateTime(2002, 01, 18);
        int expectedAge = 21;
        

        //Act
        int actualAge = _validator.GetAgeByBirthday(birthday);

        //Assert
        Assert.Equal(expectedAge, actualAge);
    }

    [Fact]
    public void GetAgeByBirthday_ClientWithAgeIs19_ReturnsNot21()
    {
        //Arrange
        var birthday = new DateTime(2004, 07, 08);
		int expectedAge = 21;

		//Act
		int actualAge = _validator.GetAgeByBirthday(birthday);

		//Assert
		Assert.NotEqual(expectedAge, actualAge);
    }

	/// <summary>
	/// Ожидаем, что метод валидации выбрасывает исключение
	/// </summary>
	[Fact]
    public void Validate_ClientCreateRequestIsInvalidWithAgeIsLess18_ThrowsClientValidationException()
    {
        //Arrange
        var requestExample = new CreateClientRequest()
        {
            LastName = "Иванов",
            FirstName = "Иван",
            MiddleName = "Иванович",
            BirthDate = new DateTime(2005, 12, 12),
            Salary = 100
        };

        //Act

        //Assert
        Assert.Throws<ClientValidationException>(() => _validator.Validate(requestExample));
    }

    /// <summary>
    /// Ожидаем, что метод валидации НЕ выбрасывает исключение
    /// </summary>
    [Fact]
    public void Validate_ClientCreateRequestIsValid_NoThrowsClientValidationException()
    {
        //Arrange
        var requestExample = new CreateClientRequest()
        {
            LastName = "Горбунова",
            FirstName = "Мария",
            MiddleName = "Дмитриевна",
            BirthDate = new DateTime(2002, 4, 15),
            Salary = 100
        };

        //Act
        var exception = Record.Exception(() => _validator.Validate(requestExample));

        //Assert
        Assert.Null(exception);
    }
}
