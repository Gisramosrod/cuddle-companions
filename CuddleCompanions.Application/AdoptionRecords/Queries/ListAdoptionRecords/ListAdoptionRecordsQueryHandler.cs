using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Queries.ListAdoptionRecords;

internal sealed class ListAdoptionRecordsQueryHandler : IRequestHandler<ListAdoptionRecordsQuery, ErrorOr<List<AdoptionRecord>>>
{
    private readonly IAdoptionRecordRepository _adoptionRecordRepository;

    public ListAdoptionRecordsQueryHandler(IAdoptionRecordRepository adoptionRecordRepository) =>
        _adoptionRecordRepository = adoptionRecordRepository;

    public async Task<ErrorOr<List<AdoptionRecord>>> Handle(ListAdoptionRecordsQuery request, CancellationToken cancellationToken)
    {
        return await _adoptionRecordRepository.ListByAdopterIdAsync(request.AdopterId, cancellationToken);
    }
}
