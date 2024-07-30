﻿using CuddleCompanions.Domain.Enums;
using CuddleCompanions.Domain.Events;
using CuddleCompanions.Domain.Events.Exceptions;
using CuddleCompanions.Domain.Repositories;
using MediatR;

namespace CuddleCompanions.Application.AdoptionRecords.Events;

internal sealed class AdoptionCanceledEventHandler : INotificationHandler<AdoptionCanceledEvent>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPetRepository _petRepository;

    public AdoptionCanceledEventHandler(IUnitOfWork unitOfWork, IPetRepository petRepository)
    {
        _unitOfWork = unitOfWork;
        _petRepository = petRepository;
    }

    public async Task Handle(AdoptionCanceledEvent notification, CancellationToken cancellationToken)
    {
        var pet = await _petRepository.GetByIdAsync(notification.PetId, cancellationToken);
        if (pet is null)
        {
            throw new PetNotFoundDomainEventException($"The pet with the Id = {notification.PetId} was not found");
        }

        pet.ChangeStatus(PetStatus.Available);
        _petRepository.Update(pet);

        await _unitOfWork.SaveChangesAsync();
    }
}
