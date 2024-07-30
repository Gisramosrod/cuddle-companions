using CuddleCompanions.Domain.Entities;

namespace CuddleCompanions.Domain.Repositories;

public interface IPetRepository
{
    Task<Pet?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Pet>> ListAvailableAsync(CancellationToken cancellationToken);
    void Add(Pet pet);
    void Update(Pet pet);
}
