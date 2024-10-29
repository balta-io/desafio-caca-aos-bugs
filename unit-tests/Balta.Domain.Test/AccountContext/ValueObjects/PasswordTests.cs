using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void ShouldFailIfPasswordIsNull()
    {
#pragma warning disable CS8625
        var act = () => Password.ShouldCreate(null);
#pragma warning restore CS8625

        Assert.ThrowsAny<InvalidPasswordException>(act);
    }
    
    [Fact]
    public void ShouldFailIfPasswordIsEmpty()
    {
        var act = () => Password.ShouldCreate(string.Empty);

        Assert.ThrowsAny<InvalidPasswordException>(act);
    }

    [Fact]
    public void ShouldFailIfPasswordIsWhiteSpace()
    {
        var act = () => Password.ShouldCreate("  ");

        Assert.ThrowsAny<InvalidPasswordException>(act);
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsLessThanMinimumChars()
    {
        var act = () => Password.ShouldCreate("1234567");

        Assert.ThrowsAny<InvalidPasswordException>(act);
    }

    [Fact]
    public void ShouldFailIfPasswordLenIsGreaterThanMaxChars()
    {
        var act = () => Password.ShouldCreate("abc@A".PadLeft(49, '0'));

        Assert.ThrowsAny<InvalidPasswordException>(act);
    }

    [Fact]
    public void ShouldHashPassword()
    {
        var result = Password.ShouldCreate("validPass123!");

        Assert.NotEmpty(result.Hash);
    }

    [Theory]
    [InlineData("validPass123!")]
    public void ShouldVerifyPasswordHash(string password)
    {
        var result = Password.ShouldCreate(password);

        var match = Password.ShouldMatch(result.Hash, password);

        Assert.True(match);
    }

    [Fact]
    public void ShouldGenerateStrongPassword()
    {
        bool IsSpecialCharacter(char c)
        {
            return !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c);
        }

        var result = Password.ShouldGenerate();

        Assert.True(
            result.Any(char.IsNumber) &&
            result.Any(char.IsLetter) &&
            result.Any(IsSpecialCharacter));
    }
    
    [Fact]
    public void ShouldImplicitConvertToString()
    {
        var result = Password.ShouldCreate("validPass123!");

        Assert.NotEmpty((string)result);
    }
    
    [Fact]
    public void ShouldReturnHashAsStringWhenCallToStringMethod()
    {
        var result = Password.ShouldCreate("validPass123!");

        Assert.Equal(result.Hash, result.ToString());
    }
    
    [Fact]
    public void ShouldMarkPasswordAsExpired()
    {
        var provider = new FakeDateTimeProvider();

        var result = Password.ShouldCreate("validPass123!", provider);

        provider.ChangeDate(DateTime.UtcNow.AddMinutes(5.1));

        Assert.True(result.IsExpired());
    }
    
    [Fact]
    public void ShouldFailIfPasswordIsExpired()
    {
        var result = Password.ShouldCreate("validPass123!");

        Assert.False(result.IsExpired());
    }
    
    [Fact]
    public void ShouldMarkPasswordAsMustChange()
    {
        var provider = new FakeDateTimeProvider();

        var result = Password.ShouldCreate("validPass123!", provider);

        provider.ChangeDate(DateTime.UtcNow.AddMinutes(5.1));

        Assert.True(result.MustChange);
    }

    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange()
    {
        var result = Password.ShouldCreate("validPass123!");

        Assert.False(result.MustChange);
    }
}