using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Enums;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Pets.Commands.CreatePet;

public sealed record CreatePetCommand(
    string Name,
    Specie Specie,
    string Breed,
    int Years,
    int Months,
    Gender Gender,
    DateTime DateArrived,
    string Description) 
    : IRequest<ErrorOr<Pet>>;
