using ErrorOr;

namespace CuddleCompanions.Domain.Errors;

public static class AdopterErrors
{
    public static readonly Error EmailNotUnique = Error.Validation(
        "AdopterErrors.EmailNotUnique", "The email is not unique");

    public static readonly Error PendingAdoptionsExist = Error.Validation(
        "AdopterErrors.PendingAdoptionsExist", "The adopter cannot start a new adoption when having pending adoptions");

    public static Error NotFound(Guid id) =>
        Error.NotFound("AdopterErrors.NotFound", $"The adopter with the Id = {id} was not found");

    public static Error AdoptionRecordNotFound(Guid adoptionRecordId) =>
        Error.NotFound("AdopterErrors.AdoptionRecordNotFound", $"The adopter does not have adoption record with Id = {adoptionRecordId}");

}
