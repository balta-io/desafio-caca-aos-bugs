using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.AccountContext.ValueObjects;

public class VerificationCode
{
    #region Constants

    private const int MinLength = 6;

    #endregion
    
    #region Constructors

    private VerificationCode(string code, DateTime expiresAtUtc, IDateTimeProvider provider)
    {
        Code = code;
        ExpiresAtUtc = expiresAtUtc;
        _provider = provider;
    }

    #endregion

    #region Factories

    public static VerificationCode ShouldCreate(IDateTimeProvider dateTimeProvider) =>
        new(
            Guid.NewGuid().ToString("N")[..MinLength].ToUpper(), 
            dateTimeProvider.UtcNow.AddMinutes(5),
            dateTimeProvider);

    #endregion

    #region Properties

    public string Code { get; }
    public DateTime? ExpiresAtUtc { get; private set; }
    public bool IsExpired => _provider.UtcNow > ExpiresAtUtc;

    private IDateTimeProvider _provider;

    public DateTime? VerifiedAtUtc { get; private set; }
    public bool IsActive => VerifiedAtUtc != null && ExpiresAtUtc is null;

    #endregion

    #region Methods

    public void ShouldVerify(string code)
    {
        if (string.IsNullOrEmpty(code))
            throw new InvalidVerificationCodeException();
        
        if (string.IsNullOrWhiteSpace(code))
            throw new InvalidVerificationCodeException();
        
        if(code.Length != MinLength)
            throw new InvalidVerificationCodeException();
        
        if(IsActive == false)
            throw new InvalidVerificationCodeException();
        
        VerifiedAtUtc = DateTime.UtcNow;
        ExpiresAtUtc = null;
    }

    #endregion
    
    #region Operators
    
    public static implicit operator string(VerificationCode verificationCode) => verificationCode.ToString();
    
    #endregion

    #region Others

    public override string ToString() =>
        IsExpired
        ? throw new Exception("The code already expired.")
        : Code;

    #endregion
}