using Balta.Domain.AccountContext.ValueObjects;

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
        var result = VerificationCode.ShouldCreate(new DateTimeProvider());

        Assert.False(result.IsActive);
    }

    [Fact]
    public void ShouldFailIfExpired()
    {
        var provider = new FakeDateTimeProvider();

        var result = VerificationCode.ShouldCreate(provider);

        provider.ChangeDate(
            provider.UtcNow.AddMinutes(6)
        );

        var act = () => result.ToString();

        Assert.Throws<Exception>(act);
    }

    [Fact]
    public void ShouldFailIfCodeIsInvalid() => Assert.Fail();

    [Fact]
    public void ShouldFailIfCodeIsLessThanSixChars() => Assert.Fail();

    [Fact]
    public void ShouldFailIfCodeIsGreaterThanSixChars() => Assert.Fail();

    [Fact]
    public void ShouldFailIfIsNotActive() => Assert.Fail();

    [Fact]
    public void ShouldFailIfIsAlreadyVerified() => Assert.Fail();

    [Fact]
    public void ShouldFailIfIsVerificationCodeDoesNotMatch() => Assert.Fail();

    [Fact]
    public void ShouldVerify() => Assert.Fail();
}