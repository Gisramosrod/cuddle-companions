using CuddleCompanions.Domain.Errors;
using CuddleCompanions.Domain.Repositories;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Command.CancelAdoption;

internal sealed class CancelAdoptionCommandHandler : IRequestHandler<CancelAdoptionCommand, ErrorOr<Success>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdopterRepository _adopterRepository;
    private readonly IAdoptionRecordRepository _adoptionRecordRepository;

    public CancelAdoptionCommandHandler(
        IUnitOfWork unitOfWork,
        IAdopterRepository adopterRepository,
        IAdoptionRecordRepository adoptionRecordRepository)
    {
        _unitOfWork = unitOfWork;
        _adopterRepository = adopterRepository;
        _adoptionRecordRepository = adoptionRecordRepository;
    }

    public async Task<ErrorOr<Success>> Handle(CancelAdoptionCommand request, CancellationToken cancellationToken)
    {
        var adopter = await _adopterRepository.GetByIdAsync(request.AdopterId, cancellationToken);
        if (adopter is null)
        {
            return AdopterErrors.NotFound(request.AdopterId);
        }

        var adoptionRecordResult = adopter.CancelAdoption(request.AdoptionRecordId);
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
