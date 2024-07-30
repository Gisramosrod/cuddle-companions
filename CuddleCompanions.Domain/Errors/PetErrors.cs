using ErrorOr;

namespace CuddleCompanions.Domain.Errors;

public static class PetErrors
{
    public static readonly Error NameEmpty = Error.Validation("PetErrors.NameEmpty", "The pet name cannot be empty");
   
    public static readonly Error NotAvailable = Error.Validation("PetErrors.NotAvailable", "Cannot adopt a pet that is not available");
    public static Error NotFound(Guid id) => Error.NotFound("PetErrors.NotFound", $"The pet with the Id = {id} was not found");
    public static Error TooLong(string petPart) => Error.Validation("PetErrors.TooLong", $"The pet {petPart} is too long");
}
