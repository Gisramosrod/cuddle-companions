namespace CuddleCompanions.Domain.Events.Exceptions;

public sealed class PetNotFoundDomainEventException : DomainEventException
{
    public PetNotFoundDomainEventException(string message) : base(message) { }
}
