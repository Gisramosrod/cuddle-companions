using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Command.InitiateAdoption;

public sealed record InitiateAdoptionCommand(Guid AdopterId, Guid PetId) : IRequest<ErrorOr<Success>>;

