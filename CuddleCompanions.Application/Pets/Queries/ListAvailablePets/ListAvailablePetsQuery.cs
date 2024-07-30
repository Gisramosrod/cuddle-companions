using CuddleCompanions.Domain.Entities;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Pets.Queries.ListAvailablePets;

public sealed record ListAvailablePetsQuery : IRequest<ErrorOr<List<Pet>>>;