using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Enums;
using CuddleCompanions.Domain.Repositories;
using CuddleCompanions.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace CuddleCompanions.Infrastructure.Repositories;

internal sealed class PetRepository : IPetRepository
{
    private readonly AppDbContext _dbContext;

    public PetRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<Pet?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbContext.Set<Pet>().Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

    public async Task<List<Pet>> ListAvailableAsync(CancellationToken cancellationToken) =>
        await _dbContext.Set<Pet>().Where(x => x.PetStatus == PetStatus.Available).ToListAsync(cancellationToken);
   
    public void Add(Pet pet) => _dbContext.Set<Pet>().Add(pet);

    public void Update(Pet pet) => _dbContext.Set<Pet>().Update(pet);
}
