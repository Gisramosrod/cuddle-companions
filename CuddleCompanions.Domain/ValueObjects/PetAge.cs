using CuddleCompanions.Domain.Errors;
using ErrorOr;

namespace CuddleCompanions.Domain.ValueObjects;

public record PetAge
{
    public int Years { get; }
    public int Months { get; }

    private PetAge() { }

    private PetAge(int years, int months)
    {
        Years = years;
        Months = months;
    }

    public static ErrorOr<PetAge> Create(int years, int months)
    {
        if (years < 0 || months < 0 || months >= 12)
        {
            return PetAgeErrors.InvalidAge;
        }

        return new PetAge(years, months);
    }
}
