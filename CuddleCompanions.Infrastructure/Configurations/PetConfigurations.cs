using CuddleCompanions.Domain.Entities;
using CuddleCompanions.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CuddleCompanions.Infrastructure.Configurations;

internal sealed class PetConfigurations : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name).HasMaxLength(50);

        builder.Property(x => x.Specie)
            .HasConversion(
            v => v.ToString(),
            v => (Specie)Enum.Parse(typeof(Specie), v));

        builder.OwnsOne(p => p.Age,
            age =>
            {
                age.Property(a => a.Years).IsRequired();
                age.Property(a => a.Months).IsRequired();
            });

        builder.Property(x => x.Gender)
          .HasConversion(
          v => v.ToString(),
          v => (Gender)Enum.Parse(typeof(Gender), v));

        builder.Property(x => x.PetStatus)
          .HasConversion(
          v => v.ToString(),
          v => (PetStatus)Enum.Parse(typeof(PetStatus), v));

        builder.Property(x => x.Description).HasMaxLength(500);
    }
}
