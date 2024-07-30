using CuddleCompanions.Api.Contracts.Common.Enums;

namespace CuddleCompanions.Api.Contracts.Pets;

public sealed record PetResponse(
    Guid Id,
    string Name,
    Specie Specie,
    string Breed,
    int Years,
    int Months,
    Gender Gender,
    PetStatus PetStatus,
    DateTime DateArrived,
    string Description);
