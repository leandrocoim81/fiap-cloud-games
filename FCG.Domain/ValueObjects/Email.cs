using FCG.Domain.Errors;
using FCG.Domain.Exceptions;
using FCG.Domain.Policies.User;

namespace FCG.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(DomainErrors.User.EmailIsNullOrWhiteSpace);

        var normalized = value.Trim().ToLowerInvariant();

        var validation = EmailPolicy.Validate(normalized);
        if (!validation.IsValid)
            throw new DomainException(validation.Error!);

        Value = normalized;
    }

    public override string ToString() => Value;

    public static implicit operator string(Email email) => email.Value;
}
