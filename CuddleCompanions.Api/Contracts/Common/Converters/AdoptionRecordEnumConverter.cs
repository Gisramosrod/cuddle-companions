using CuddleCompanions.Api.Contracts.Common.Enums;

namespace CuddleCompanions.Api.Contracts.Common.Converters;

public static class AdoptionRecordEnumConverter
{
    public static AdoptionStatus ToDtoAdoptionStatus(Domain.Enums.AdoptionStatus domainAdoptionStatus)
    {
        return (AdoptionStatus)Enum.Parse(
            typeof(AdoptionStatus),
            domainAdoptionStatus.ToString());
    }
}
