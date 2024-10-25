using Balta.Domain.AccountContext.ValueObjects;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{
    [Fact]
    public void ShouldLowerCaseEmail()
    {
        const string expected = "myemail@gmail.com";

        var result = Email.ShouldCreate("MYEMAIL@GMAIL.COM", FakeDateTimeProvider.Default);

        Assert.Equal(expected, result.Address);
    }
    
    [Fact]
    public void ShouldTrimEmail()
    {
        const string expected = "myemail@gmail.com";

        var result = Email.ShouldCreate(" " + expected + " ", FakeDateTimeProvider.Default);

        Assert.Equal(expected, result.Address);
    }

    [Fact]
    public void ShouldFailIfEmailIsNull()
    {
#pragma warning disable CS8625
        var act = () 
            => Email.ShouldCreate(null, FakeDateTimeProvider.Default);
#pragma warning restore CS8625

        Assert.ThrowsAny<Exception>(act);
    }

    [Fact]
    public void ShouldFailIfEmailIsEmpty()
    {
        var act = ()
            => Email.ShouldCreate(string.Empty, FakeDateTimeProvider.Default);

        Assert.ThrowsAny<Exception>(act);
    }

    [Fact]
    public void ShouldFailIfEmailIsInvalid()
    {
        const string invalidEmail = "invalidemail";

        var act = ()
            => Email.ShouldCreate(invalidEmail, FakeDateTimeProvider.Default);

        Assert.ThrowsAny<Exception>(act);
    }

    [Fact]
    public void ShouldPassIfEmailIsValid()
    {
        const string validEmail = "myemail@gmail.com";

        var result = Email.ShouldCreate(validEmail, FakeDateTimeProvider.Default);

        Assert.NotEmpty(result.Address);
    }

    [Fact]
    public void ShouldHashEmailAddress()
    {
        const string validEmail = "myemail@gmail.com";

        var result = Email.ShouldCreate(validEmail, FakeDateTimeProvider.Default);

        Assert.NotEmpty(result.Hash);
    }

    [Fact]
    public void ShouldExplicitConvertFromString()
    {
    }

    [Fact]
    public void ShouldExplicitConvertToString() => Assert.Fail();
    
    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod() => Assert.Fail();
}