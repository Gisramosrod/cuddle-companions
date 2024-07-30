using CuddleCompanions.Domain.Common;
using CuddleCompanions.Domain.Enums;
using CuddleCompanions.Domain.Errors;
using ErrorOr;

namespace CuddleCompanions.Domain.Entities;

public class AdoptionRecord : Entity
{
    public Guid AdopterId { get; private set; }
    public Guid PetId { get; private set; }
    public DateTime AdoptionStartDate { get; private set; }
    public AdoptionStatus Status { get; private set; }

    private AdoptionRecord() { }

    internal AdoptionRecord(
        Guid id,
        Guid adopterId,
        Guid petId,
        DateTime adoptionStartDate,
        AdoptionStatus status)
        : base(id)
    {
        AdopterId = adopterId;
        PetId = petId;
        AdoptionStartDate = adoptionStartDate;
        Status = status;
    }

    internal ErrorOr<Success> CompleteAdoption()
    {
        if (Status != AdoptionStatus.Pending)
        {
            return AdoptionRecordErrors.CompleteNotPending;
        }
         
        Status = AdoptionStatus.Completed;
        return Result.Success;
    }

    internal ErrorOr<Success> CancelAdoption()
    {
        if (Status != AdoptionStatus.Pending)
        {
            return AdoptionRecordErrors.CancelNotPending;
        }

        Status = AdoptionStatus.Cancelled;
        return Result.Success;
    }
}
