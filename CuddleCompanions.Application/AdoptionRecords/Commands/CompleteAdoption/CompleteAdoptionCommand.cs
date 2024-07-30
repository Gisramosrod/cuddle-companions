using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Command.CompleteAdoption;

public sealed record CompleteAdoptionCommand(Guid AdopterId, Guid AdoptionRecordId) : IRequest<ErrorOr<Success>>;