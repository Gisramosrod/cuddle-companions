using ErrorOr;

namespace CuddleCompanions.Domain.Errors;

public static class AddressErrors
{
    public static Error Empty(string addressPart) => Error.Validation("AddressErrors.Empty", $"{addressPart} cannot be empty");
}
