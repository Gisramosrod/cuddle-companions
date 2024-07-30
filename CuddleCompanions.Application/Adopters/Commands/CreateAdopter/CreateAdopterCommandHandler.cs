using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Errors;
using CuddleCompanions.Domain.Repositories;
using CuddleCompanions.Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Adopters.Commands.CreateAdopter;

internal sealed class CreateAdopterCommandHandler : IRequestHandler<CreateAdopterCommand, ErrorOr<Adopter>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdopterRepository _adopterRepository;

    public CreateAdopterCommandHandler(IUnitOfWork unitOfWork, IAdopterRepository adopterRepository)
    {
        _unitOfWork = unitOfWork;
        _adopterRepository = adopterRepository;
    }

    public async Task<ErrorOr<Adopter>> Handle(CreateAdopterCommand request, CancellationToken cancellationToken)
    {
        if (!await _adopterRepository.IsEmailUniqueAsync(request.Email, cancellationToken))
        {
            return AdopterErrors.EmailNotUnique;
        }

        var emailResult = Email.Create(request.Email);
        if (emailResult.IsError)
        {
            return emailResult.Errors;
        }

        var phoneResult = Phone.Create(request.PhoneCountryCode, request.PhoneNumber);
        if (phoneResult.IsError)
        {
            return phoneResult.Errors;
        }

        var addressResult = Address.Create(
            request.AddressStreet,
            request.AddressCity,
            request.AddressState,
            request.AddressPostalCode,
            request.AddressCountry);
        if (addressResult.IsError)
        {
            return addressResult.Errors;
        }

        var adopter = Adopter.Create(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            emailResult.Value,
            phoneResult.Value,
            addressResult.Value);

        _adopterRepository.Add(adopter);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return adopter;
    }
}
