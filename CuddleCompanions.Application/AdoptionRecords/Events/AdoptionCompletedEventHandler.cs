using CuddleCompanions.Domain.Enums;
using CuddleCompanions.Domain.Events;
using CuddleCompanions.Domain.Events.Exceptions;
using CuddleCompanions.Domain.Repositories;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Events;

internal sealed class AdoptionCompletedEventHandler : INotificationHandler<AdoptionCompletedEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPetRepository _petRepository;

    public AdoptionCompletedEventHandler(IUnitOfWork unitOfWork, IPetRepository petRepository)
    {
        _unitOfWork = unitOfWork;
        _petRepository = petRepository;
    }

    public async Task Handle(AdoptionCompletedEvent notification, CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetByIdAsync(notification.PetId, cancellationToken);
        if (pet is null)
        {
            throw new PetNotFoundDomainEventException($"The pet with the Id = {notification.PetId} was not found");
        }

        pet.ChangeStatus(PetStatus.Adopted);
        _petRepository.Update(pet);

        await _unitOfWork.SaveChangesAsync();   
    }
}
