using CuddleCompanions.Domain.Errors;
using ErrorOr;

namespace CuddleCompanions.Domain.ValueObjects;

public record Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string PostalCode { get; }
    public string Country { get; }

    private Address() { }

    private Address(string street, string city, string state, string postalCode, string country)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
        Country = country;
    }

    public static ErrorOr<Address> Create(string street, string city, string state, string postalCode, string country)
    {
        if (string.IsNullOrWhiteSpace(street)) return AddressErrors.Empty(nameof(Street));
        if (string.IsNullOrWhiteSpace(city)) return AddressErrors.Empty(nameof(City));
        if (string.IsNullOrWhiteSpace(state)) return AddressErrors.Empty(nameof(State));
        if (string.IsNullOrWhiteSpace(postalCode)) return AddressErrors.Empty(nameof(PostalCode));
        if (string.IsNullOrWhiteSpace(country)) return AddressErrors.Empty(nameof(Country));

        return new Address(street, city, state, postalCode, country);
    }
}
