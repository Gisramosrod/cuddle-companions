using CuddleCompanions.Domain.Common;

namespace CuddleCompanions.Domain.Events;

public sealed record AdoptionInitiatedEvent(Guid PetId) : IDomainEvent;
