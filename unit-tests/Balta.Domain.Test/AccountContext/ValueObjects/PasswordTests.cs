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
        var act = () => Password.ShouldCreate("abc@A".PadLeft(40, '0'));

        Assert.ThrowsAny<InvalidPasswordException>(act);
    }

    [Fact]
    public void ShouldHashPassword()
    {
        var result = Password.ShouldCreate("validPass123!");

        Assert.NotEmpty(result.Hash);
    }

    [Fact]
    public void ShouldVerifyPasswordHash()
    {
        var result = Password.ShouldCreate("validPass123!");

        Assert.Equal("abc1234", result.Hash);
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
        var result = Password.ShouldCreate("validPass123!");

        Assert.True(result.IsExpired());
    }
    
    [Fact]
    public void ShouldFailIfPasswordIsExpired()
    {
        Assert.Fail();
    }
    
    [Fact]
    public void ShouldMarkPasswordAsMustChange()
    {
        Assert.Fail();
    }

    [Fact]
    public void ShouldFailIfPasswordIsMarkedAsMustChange()
    {
        Assert.Fail();
    }
}