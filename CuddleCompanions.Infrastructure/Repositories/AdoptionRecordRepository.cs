using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Repositories;
using CuddleCompanions.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CuddleCompanions.Infrastructure.Repositories;

internal sealed class AdoptionRecordRepository : IAdoptionRecordRepository
{
    private readonly AppDbContext _dbContext;

    public AdoptionRecordRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<AdoptionRecord>> ListByAdopterIdAsync(Guid adopterId, CancellationToken cancellationToken) =>
        await _dbContext.Set<AdoptionRecord>().Where(x => x.AdopterId == adopterId).ToListAsync(cancellationToken);

    public void Add(AdoptionRecord adoptionRecord) => _dbContext.Set<AdoptionRecord>().Add(adoptionRecord);

    public void Update(AdoptionRecord adoptionRecord) => _dbContext.Set<AdoptionRecord>().Update(adoptionRecord);
}
