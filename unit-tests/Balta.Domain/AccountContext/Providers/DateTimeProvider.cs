using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.AccountContext.Providers;

public class DateTimeProvider
    : IDateTimeProvider
{
    /// <summary>
    /// Uses 'DateTime.UtcNow' internally.
    /// </summary>
    public static IDateTimeProvider Default { get; } 
        = new DateTimeProvider();

    private DateTimeProvider() { }

    public DateTime UtcNow => DateTime.UtcNow;
}
