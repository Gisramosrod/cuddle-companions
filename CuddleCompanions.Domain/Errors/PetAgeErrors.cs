using ErrorOr;

namespace CuddleCompanions.Domain.Errors;

public static class PetAgeErrors
{
    public static readonly Error InvalidAge = Error.Validation("PetAgeErrors.InvalidAge", "Invalid age values");
}
