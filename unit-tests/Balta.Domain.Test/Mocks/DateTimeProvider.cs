using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.Test.Mocks;

internal class DateTimeProvider
    : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
