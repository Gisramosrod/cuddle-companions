using CuddleCompanions.Domain.Entities;
using ErrorOr;
using MediatR;

namespace CuddleCompanions.Application.Adopters.Commands.CreateAdopter;

public sealed record CreateAdopterCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneCountryCode,
    string PhoneNumber,
    string AddressStreet,
    string AddressCity,
    string AddressState,
    string AddressPostalCode,
    string AddressCountry)
    : IRequest<ErrorOr<Adopter>>;