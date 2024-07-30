using CuddleCompanions.Domain.Entities;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Pets.Queries.GetPetById;

public sealed record GetPetByIdQuery(Guid Id) : IRequest<ErrorOr<Pet>>;