using Balta.Domain.SharedContext.Extensions;

namespace Balta.Domain.Test.SharedContext.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("dGVzdA==", "test")]
    [InlineData("dGVzdDEyMyFAQUJD", "test123!@ABC")]
    public void ShouldGenerateBase64FromString(string base64, string text)
    {
        var currentBase65 = text.ToBase64();

        Assert.Equal(base64, currentBase65);
    }
}