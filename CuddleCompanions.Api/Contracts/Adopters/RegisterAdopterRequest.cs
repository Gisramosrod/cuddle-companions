namespace CuddleCompanions.Api.Contracts.Adopters;

public sealed record RegisterAdopterRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneCountryCode,
    string PhoneNumber,
    string AddressStreet,
    string AddressCity,
    string AddressState,
    string AddressPostalCode,
    string AddressCountry);
