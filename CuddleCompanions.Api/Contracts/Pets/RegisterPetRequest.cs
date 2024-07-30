using CuddleCompanions.Api.Contracts.Common.Enums;

namespace CuddleCompanions.Api.Contracts.Pets;

public sealed record RegisterPetRequest(
    string Name,
    Specie Specie, 
    string Breed, 
    int Years, 
    int Months, 
    Gender Gender, 
    DateTime DateArrived, 
    string Description);

