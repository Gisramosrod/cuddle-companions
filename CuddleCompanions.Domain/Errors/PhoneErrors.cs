using ErrorOr;

namespace CuddleCompanions.Domain.Errors;

public static class PhoneErrors
{
    public static Error Empty(string phonePart) => Error.Validation("PhoneErrors.Empty", $"{phonePart} cannot be empty");
}
