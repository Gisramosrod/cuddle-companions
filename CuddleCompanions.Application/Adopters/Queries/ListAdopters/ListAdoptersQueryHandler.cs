using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Adopters.Queries.ListAdopters;

internal sealed class ListAdoptersQueryHandler : IRequestHandler<ListAdoptersQuery, ErrorOr<List<Adopter>>>
{
    private readonly IAdopterRepository _adopterRepository;

    public ListAdoptersQueryHandler(IAdopterRepository adopterRepository) => _adopterRepository = adopterRepository;

    public async Task<ErrorOr<List<Adopter>>> Handle(ListAdoptersQuery request, CancellationToken cancellationToken)
    {
        return await _adopterRepository.ListAsync(cancellationToken);
    }
}
