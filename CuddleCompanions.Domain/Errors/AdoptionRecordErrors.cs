using ErrorOr;

namespace CuddleCompanions.Domain.Errors;

public static class AdoptionRecordErrors
{
    public static readonly Error CompleteNotPending = Error.Validation(
        "AdoptionRecordErrors.CompleteNotPending",
        "Cannot complete the adoption of an Adoption Record that is not in Pending status.");

    public static readonly Error CancelNotPending = Error.Validation(
        "AdoptionRecordErrors.CancelNotPending", "Cannot complete the adoption of an Adoption Record that is not in Pending status.");
}
