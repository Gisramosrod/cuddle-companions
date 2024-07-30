using CuddleCompanions.Domain.Errors;
using ErrorOr;

namespace CuddleCompanions.Domain.ValueObjects;

public record Phone
{
    public string CountryCode { get; }
    public string Number { get; }

    private Phone() { }

    private Phone(string countryCode, string number)
    {
        CountryCode = countryCode;
        Number = number;
    }

    public static ErrorOr<Phone> Create(string countryCode, string number)
    {
        if (string.IsNullOrWhiteSpace(countryCode)) return PhoneErrors.Empty(nameof(CountryCode));
        if (string.IsNullOrWhiteSpace(number)) return PhoneErrors.Empty(nameof(Number));

        return new Phone(countryCode, number);
    }
}
