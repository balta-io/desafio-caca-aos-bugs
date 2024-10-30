using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("FAKE@EMAIL.COM", "fake@email.com")]
    [InlineData("FaKe@EmAIL.COM", "fake@email.com")]
    [InlineData("Fake@EmaiL.COM", "fake@email.com")]
    public void ShouldLowerCaseEmail(string email, string expected)
    {
        var result = Email.ShouldCreate(email, FakeDateTimeProvider.Default);

        Assert.Equal(expected, result.Address);
    }

    [Theory]
    [InlineData(" fake@email.com", "fake@email.com")]
    [InlineData("  fake@email.com ", "fake@email.com")]
    [InlineData("fake@email.com ", "fake@email.com")]
    public void ShouldTrimEmail(string email, string expected)
    {
        var result = Email.ShouldCreate(email, FakeDateTimeProvider.Default);

        Assert.Equal(expected, result.Address);
    }

    [Fact]
    public void ShouldFailIfEmailIsNull()
    {
#pragma warning disable CS8625
        var act = () => Email.ShouldCreate(null, FakeDateTimeProvider.Default);
#pragma warning restore CS8625

        Assert.ThrowsAny<InvalidEmailException>(act);
    }

    [Fact]
    public void ShouldFailIfEmailIsEmpty()
    {
        var act = ()
            => Email.ShouldCreate(string.Empty, FakeDateTimeProvider.Default);

        Assert.ThrowsAny<Exception>(act);
    }

    [Theory]
    [InlineData("fake@email")]
    [InlineData("fakeemail.com")]
    [InlineData("fake#email.com")]
    [InlineData("fake!email.com")]
    public void ShouldFailIfEmailIsInvalid(string invalidEmail)
    {
        var act = ()
            => Email.ShouldCreate(invalidEmail, FakeDateTimeProvider.Default);

        Assert.ThrowsAny<InvalidEmailException>(act);
    }

    [Theory]
    [InlineData("fake@email.com.br")]
    [InlineData("teste@gmail.com")]
    [InlineData("teste@hotmail.com")]
    [InlineData("teste@hotmail.com.ar")]
    public void ShouldPassIfEmailIsValid(string validEmail)
    {
        var result = Email.ShouldCreate(validEmail, FakeDateTimeProvider.Default);

        Assert.NotEmpty(result.Address);
    }

    [Theory]
    [InlineData("test@email.com")]
    [InlineData("test@fakemail.com")]
    [InlineData("test@fakemail.com.br")]
    public void ShouldHashEmailAddress(string validEmail)
    {
        var result = Email.ShouldCreate(validEmail, FakeDateTimeProvider.Default);

        Assert.NotEmpty(result.Hash);
    }

    [Fact]
    public void ShouldExplicitConvertFromString()
    {
        const string validEmail = "myemail@gmail.com";

        Assert.NotNull((Email)validEmail);
    }

    [Fact]
    public void ShouldExplicitConvertToString()
    {
        const string validEmail = "myemail@gmail.com";

        var result =
            Email.ShouldCreate(validEmail, FakeDateTimeProvider.Default);

        Assert.NotEmpty((string)result);
    }

    [Theory]
    [InlineData("fake@email.com.br")]
    [InlineData("teste@gmail.com")]
    [InlineData("teste@hotmail.com")]
    [InlineData("teste@hotmail.com.ar")]
    public void ShouldReturnEmailWhenCallToStringMethod(string email)
    {
        var result =
            Email.ShouldCreate(email, FakeDateTimeProvider.Default);

        Assert.Equal(email, result.ToString());
    }
}