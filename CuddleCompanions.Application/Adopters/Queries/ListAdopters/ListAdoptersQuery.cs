using CuddleCompanions.Domain.Entities;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Adopters.Queries.ListAdopters;

public sealed record ListAdoptersQuery : IRequest<ErrorOr<List<Adopter>>>;
