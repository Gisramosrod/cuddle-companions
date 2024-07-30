using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Repositories;
using CuddleCompanions.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CuddleCompanions.Infrastructure.Repositories;

internal sealed class AdopterRepository : IAdopterRepository
{
    private readonly AppDbContext _dbContext;

    public AdopterRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<Adopter?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbContext.Set<Adopter>().Where(x => x.Id == id).Include(x => x.AdoptionRecords).FirstOrDefaultAsync(cancellationToken);

    public async Task<List<Adopter>> ListAsync(CancellationToken cancellationToken) =>
        await _dbContext.Set<Adopter>().Include(x => x.AdoptionRecords).ToListAsync(cancellationToken);

    public void Add(Adopter adopter) => _dbContext.Set<Adopter>().Add(adopter);

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken) =>
        !await _dbContext.Set<Adopter>().Where(x => x.Email.Value == email).AnyAsync(cancellationToken);
}
