using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CuddleCompanions.Infrastructure.Configurations;

internal sealed class AdoptionRecordConfiguration : IEntityTypeConfiguration<AdoptionRecord>
{
    public void Configure(EntityTypeBuilder<AdoptionRecord> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.AdopterId);

        builder.Property(x => x.PetId);

        builder.Property(x => x.Status)
            .HasConversion(
            v => v.ToString(),
            v => (AdoptionStatus)Enum.Parse(typeof(AdoptionStatus), v));
    }
}
