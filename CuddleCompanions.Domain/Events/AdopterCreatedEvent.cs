using CuddleCompanions.Domain.Common;
using CuddleCompanions.Domain.ValueObjects;

namespace CuddleCompanions.Domain.Events;

public sealed record AdopterCreatedEvent(Email Email, string FirstName, string LastName) : IDomainEvent;
