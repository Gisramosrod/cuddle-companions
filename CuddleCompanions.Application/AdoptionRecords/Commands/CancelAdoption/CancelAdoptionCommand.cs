using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Command.CancelAdoption;

public sealed record CancelAdoptionCommand(Guid AdopterId, Guid AdoptionRecordId) : IRequest<ErrorOr<Success>>;

