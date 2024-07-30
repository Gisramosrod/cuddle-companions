using CuddleCompanions.Domain.Common;

namespace CuddleCompanions.Domain.Events;

public sealed record AdoptionCanceledEvent(Guid PetId) : IDomainEvent;


