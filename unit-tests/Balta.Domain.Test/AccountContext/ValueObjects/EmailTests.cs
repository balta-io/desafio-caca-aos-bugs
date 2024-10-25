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
    public void ShouldTrimEmail() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfEmailIsNull() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfEmailIsEmpty() => Assert.Fail();
    
    [Fact]
    public void ShouldFailIfEmailIsInvalid() => Assert.Fail();
    
    [Fact]
    public void ShouldPassIfEmailIsValid() => Assert.Fail();
    
    [Fact]
    public void ShouldHashEmailAddress() => Assert.Fail();
    
    [Fact]
    public void ShouldExplicitConvertFromString() => Assert.Fail();
    
    [Fact]
    public void ShouldExplicitConvertToString() => Assert.Fail();
    
    [Fact]
    public void ShouldReturnEmailWhenCallToStringMethod() => Assert.Fail();
}