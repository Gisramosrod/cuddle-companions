using CuddleCompanions.Domain.Entities;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Adopters.Queries.GetAdopterById;

public sealed record GetAdopterByIdQuery(Guid Id) : IRequest<ErrorOr<Adopter>>;