using CuddleCompanions.Domain.Entities;

namespace CuddleCompanions.Domain.Repositories;

public interface IAdopterRepository
{
    Task<Adopter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Adopter>> ListAsync(CancellationToken cancellationToken);
    void Add(Adopter adopter);
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);
}
