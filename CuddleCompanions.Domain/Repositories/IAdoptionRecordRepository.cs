using CuddleCompanions.Domain.Entities;

namespace CuddleCompanions.Domain.Repositories;
public interface IAdoptionRecordRepository
{
    Task<List<AdoptionRecord>> ListByAdopterIdAsync(Guid adopterId, CancellationToken cancellationToken);
    void Add(AdoptionRecord adoptionRecord);
    void Update(AdoptionRecord adoptionRecord);
}
