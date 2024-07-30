using CuddleCompanions.Domain.Errors;
using ErrorOr;

namespace CuddleCompanions.Domain.ValueObjects;

public record Email
{
    public string Value { get; }

    private Email() { }

    private Email(string value) => Value = value;

    public static ErrorOr<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return EmailErrors.Empty;
        }

        if (email.Split('@').Length != 2)
        {
            return EmailErrors.InvalidFormat;
        }

        return new Email(email);
    }
}
