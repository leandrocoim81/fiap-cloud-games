using FCG.Domain.Errors;
using FCG.Domain.Exceptions;
using System.Net.Mail;

namespace FCG.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        var normalized = value?.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(normalized))
            throw new DomainException(DomainErrors.User.EmailIsNullOrWhiteSpace);

        try
        {
            _ = new MailAddress(normalized);
        }
        catch
        {
            throw new DomainException(DomainErrors.User.InvalidEmail);
        }

        Value = normalized;
    }

    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;
}
