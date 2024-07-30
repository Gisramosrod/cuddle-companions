using CuddleCompanions.Domain.Enums;
using CuddleCompanions.Domain.Errors;
using CuddleCompanions.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Command.InitiateAdoption;

internal sealed class InitiateAdoptionCommandHandler : IRequestHandler<InitiateAdoptionCommand, ErrorOr<Success>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdoptionRecordRepository _adoptionRecordRepository;
    private readonly IAdopterRepository _adopterRepository;
    private readonly IPetRepository _petRepository;

    public InitiateAdoptionCommandHandler(
        IUnitOfWork unitOfWork,
        IAdoptionRecordRepository adoptionRecordRepository,
        IAdopterRepository adopterRepository,
        IPetRepository petRepository)
    {
        _unitOfWork = unitOfWork;
        _adoptionRecordRepository = adoptionRecordRepository;
        _adopterRepository = adopterRepository;
        _petRepository = petRepository;
    }

    public async Task<ErrorOr<Success>> Handle(InitiateAdoptionCommand request, CancellationToken cancellationToken)
    {
        var adopter = await _adopterRepository.GetByIdAsync(request.AdopterId, cancellationToken);
        if (adopter is null)
        {
            return AdopterErrors.NotFound(request.AdopterId);
        }

        var pet = await _petRepository.GetByIdAsync(request.PetId, cancellationToken);
        if (pet is null)
        {
            return PetErrors.NotFound(request.PetId);
        }
        if (pet.PetStatus != PetStatus.Available)
        {
            return PetErrors.NotAvailable;
        }

        var adoptionRecordResult = adopter.InitiateAdoption(request.PetId);
        if (adoptionRecordResult.IsError)
        {
            return adoptionRecordResult.Errors;
        }

        var adotionRecord = adoptionRecordResult.Value;

        _adoptionRecordRepository.Add(adotionRecord);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
