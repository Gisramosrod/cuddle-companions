using CuddleCompanions.Domain.Errors;
using CuddleCompanions.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Command.CompleteAdoption;

internal sealed class CompleteAdoptionCommandHandler : IRequestHandler<CompleteAdoptionCommand, ErrorOr<Success>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdopterRepository _adopterRepository;
    private readonly IAdoptionRecordRepository _adoptionRecordRepository;

    public CompleteAdoptionCommandHandler(
        IUnitOfWork unitOfWork,
        IAdopterRepository adopterRepository,
        IAdoptionRecordRepository adoptionRecordRepository)
    {
        _unitOfWork = unitOfWork;
        _adopterRepository = adopterRepository;
        _adoptionRecordRepository = adoptionRecordRepository;
    }

    public async Task<ErrorOr<Success>> Handle(CompleteAdoptionCommand request, CancellationToken cancellationToken)
    {
        var adopter = await _adopterRepository.GetByIdAsync(request.AdopterId, cancellationToken);
        if (adopter is null)
        {
            return AdopterErrors.NotFound(request.AdopterId);
        }

        var adoptionRecordResult = adopter.CompleteAdoption(request.AdoptionRecordId);
        if (adoptionRecordResult.IsError)
        {
            return adoptionRecordResult.Errors;
        }

        var adopterRecord = adoptionRecordResult.Value;

        _adoptionRecordRepository.Update(adopterRecord);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success;
    }
}
