using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Errors;
using CuddleCompanions.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Pets.Queries.GetPetById;

internal sealed class GetPetByIdQueryHandler : IRequestHandler<GetPetByIdQuery, ErrorOr<Pet>>
{
    private readonly IPetRepository _petRepository;

    public GetPetByIdQueryHandler(IPetRepository petRepository) => _petRepository = petRepository;
    
    public async Task<ErrorOr<Pet>> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetByIdAsync(request.Id, cancellationToken);
        if (pet == null)
        {
            return PetErrors.NotFound(request.Id);
        }

        return pet;
    }
}
