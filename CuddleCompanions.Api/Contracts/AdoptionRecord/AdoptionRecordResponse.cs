using CuddleCompanions.Api.Contracts.Common.Enums;

namespace CuddleCompanions.Api.Contracts.AdoptionRecord;

public sealed record AdoptionRecordResponse(
    Guid Id,
    Guid AdopterId,
    Guid PetId,
    DateTime AdoptionStartDate,
    AdoptionStatus Status);
