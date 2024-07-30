using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Repositories;
using CuddleCompanions.Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Pets.Commands.CreatePet;

internal sealed class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, ErrorOr<Pet>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPetRepository _petRepository;

    public CreatePetCommandHandler(IUnitOfWork unitOfWork, IPetRepository petRepository)
    {
        _unitOfWork = unitOfWork;
        _petRepository = petRepository;
    }

    public async Task<ErrorOr<Pet>> Handle(CreatePetCommand request, CancellationToken cancellationToken)
    {
        var petAgeResult = PetAge.Create(request.Years, request.Months);
        if (petAgeResult.IsError)
        {
            return petAgeResult.Errors;
        }

        var petResult = Pet.Create(
            Guid.NewGuid(),
            request.Name,
            request.Specie,
            request.Breed,
            petAgeResult.Value,
            request.Gender,
            request.DateArrived,
            request.Description);
        if (petResult.IsError)
        {
            return petResult.Errors;
        }

        var pet = petResult.Value;

        _petRepository.Add(pet);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return pet;
    }
}
