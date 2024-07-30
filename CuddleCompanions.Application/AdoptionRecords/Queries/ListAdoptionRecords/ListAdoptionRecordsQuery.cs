using CuddleCompanions.Domain.Entities;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Queries.ListAdoptionRecords;

public sealed record ListAdoptionRecordsQuery(Guid AdopterId) : IRequest<ErrorOr<List<AdoptionRecord>>>;
