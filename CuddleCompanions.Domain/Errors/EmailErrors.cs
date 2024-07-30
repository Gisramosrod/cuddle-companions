using ErrorOr;

namespace CuddleCompanions.Domain.Errors;

public static class EmailErrors
{
    public static readonly Error Empty = Error.Validation("EmailErrors.Empty", "The email cannot be empty");

    public static readonly Error InvalidFormat = Error.Validation("EmailErrors.InvalidFormat", "The email format is invalid");
}
