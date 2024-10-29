namespace Dima.Core.Models.Account;

public class UserInfo
{
    public long UserId { get; set; }
    public RoleClaim[] Roles { get; set; } = [];
}

public class RoleClaim
{
    public string? Issuer { get; set; }
    public string? OriginalIssuer { get; set; }
    public string? Type { get; set; }
    public string? Value { get; set; }
    public string? ValueType { get; set; }
}