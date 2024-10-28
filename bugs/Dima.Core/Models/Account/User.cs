namespace Dima.Core.Models.Account;

public class User
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsEmailConfirmed { get; set; }
    public Dictionary<string, string> Claims { get; set; } = [];
}