using CuddleCompanions.Api.Contracts.Common.Enums;

namespace CuddleCompanions.Api.Contracts.Common.Converters;

public static class PetEnumConverter
{
    public static Domain.Enums.Specie ToDomainSpecie(this Specie apiSpecie)
    {
        return (Domain.Enums.Specie)Enum.Parse(
            typeof(Domain.Enums.Specie),
            apiSpecie.ToString());
    }
    public static Domain.Enums.Gender ToDomainGender(this Gender apiGender)
    {
        return (Domain.Enums.Gender)Enum.Parse(
            typeof(Domain.Enums.Gender),
            apiGender.ToString());
    }
    public static Domain.Enums.PetStatus ToDomainPetStatus(this PetStatus apiPetStatus)
    {
        return (Domain.Enums.PetStatus)Enum.Parse(
            typeof(Domain.Enums.PetStatus),
            apiPetStatus.ToString());
    }

    public static Specie ToDtoSpecie(Domain.Enums.Specie domainSpecie)
    {
        return (Specie)Enum.Parse(
            typeof(Specie),
            domainSpecie.ToString());
    }
    public static Gender ToDtoGender(Domain.Enums.Gender domainGender)
    {
        return (Gender)Enum.Parse(
            typeof(Gender),
            domainGender.ToString());
    }
    public static PetStatus ToDtoPetStatus(Domain.Enums.PetStatus domainPetStatus)
    {
        return (PetStatus)Enum.Parse(
            typeof(PetStatus),
            domainPetStatus.ToString());
    }   
}
