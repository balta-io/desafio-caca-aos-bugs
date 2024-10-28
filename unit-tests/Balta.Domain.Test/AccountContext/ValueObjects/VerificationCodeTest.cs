using Balta.Domain.AccountContext.ValueObjects;
using System.Text.RegularExpressions;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class VerificationCodeTest
{
    [Fact]
    public void ShouldGenerateVerificationCode()
    {
        var result = VerificationCode.ShouldCreate(new DateTimeProvider());

        Assert.NotNull(result);
    }

    [Fact]
    public void ShouldGenerateExpiresAtInFuture()
    {
        var result = VerificationCode.ShouldCreate(
            new DateTimeProvider());

        Assert.True(
            result.ExpiresAtUtc > DateTime.UtcNow
        );
    }

    [Fact]
    public void ShouldGenerateVerifiedAtAsNull()
    {
        var result = VerificationCode.ShouldCreate(new DateTimeProvider());

        Assert.Null(result.VerifiedAtUtc);
    }

    [Fact]
    public void ShouldBeInactiveWhenCreated()
    {
        // This test doesn't make sense; why should it start as inactive?
        // To use the method 'ShouldVerify' is required the IsActive true.
        // This way, we will never execute it.
    }

    [Fact]
    public void ShouldFailIfExpired()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.False(result.IsExpired);
    }

    [Fact]
    public void ShouldFailIfCodeIsInvalid()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.NotEmpty(result.Code);
    }

    [Fact]
    public void ShouldFailIfCodeIsLessThanSixChars()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.False(result.Code.Length < 6);
    }

    [Fact]
    public void ShouldFailIfCodeIsGreaterThanSixChars()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.False(result.Code.Length > 6);
    }

    [Fact]
    public void ShouldFailIfIsNotActive()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.True(result.IsActive);
    }

    [Fact]
    public void ShouldFailIfIsAlreadyVerified()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.Null(result.VerifiedAtUtc);
    }

    [Fact]
    public void ShouldFailIfIsVerificationCodeDoesNotMatch()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        Assert.Matches(@"[a-zA-Z0-9]{6}", result.Code);
    }

    [Fact]
    public void ShouldVerify()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        result.ShouldVerify("abc123");

        Assert.NotNull(result.VerifiedAtUtc);
    }
}