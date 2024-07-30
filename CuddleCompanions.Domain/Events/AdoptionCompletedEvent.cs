using CuddleCompanions.Domain.Common;

namespace CuddleCompanions.Domain.Events;

public sealed record AdoptionCompletedEvent(Guid PetId) : IDomainEvent;
