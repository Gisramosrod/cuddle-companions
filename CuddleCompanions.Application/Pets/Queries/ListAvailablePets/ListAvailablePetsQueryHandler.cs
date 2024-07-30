using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Pets.Queries.ListAvailablePets;

internal sealed class ListAvailablePetsQueryHandler : IRequestHandler<ListAvailablePetsQuery, ErrorOr<List<Pet>>>
{
    private readonly IPetRepository _petRepository;

    public ListAvailablePetsQueryHandler(IPetRepository petRepository) =>
        _petRepository = petRepository;

    public async Task<ErrorOr<List<Pet>>> Handle(ListAvailablePetsQuery request, CancellationToken cancellationToken)
    {
        return await _petRepository.ListAvailableAsync(cancellationToken);
    }
}
