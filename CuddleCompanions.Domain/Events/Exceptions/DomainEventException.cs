namespace CuddleCompanions.Domain.Events.Exceptions;

public abstract class DomainEventException : Exception
{
    protected DomainEventException(string message) : base(message) { }
}
