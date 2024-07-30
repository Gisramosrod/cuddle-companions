using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Errors;
using CuddleCompanions.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Adopters.Queries.GetAdopterById;

internal sealed class GetAdopterByIdQueryHandler : IRequestHandler<GetAdopterByIdQuery, ErrorOr<Adopter>>
{
    private readonly IAdopterRepository _adopterRepository;

    public GetAdopterByIdQueryHandler(IAdopterRepository adopterRepository) =>
        _adopterRepository = adopterRepository;

    public async Task<ErrorOr<Adopter>> Handle(GetAdopterByIdQuery request, CancellationToken cancellationToken)
    {
        var adopter = await _adopterRepository.GetByIdAsync(request.Id, cancellationToken);
        if (adopter is null)
        {
            return AdopterErrors.NotFound(request.Id);
        }

        return adopter;
    }
}
