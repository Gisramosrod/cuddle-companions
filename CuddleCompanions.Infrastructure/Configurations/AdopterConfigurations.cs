using CuddleCompanions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CuddleCompanions.Infrastructure.Configurations;

internal sealed class AdopterConfigurations : IEntityTypeConfiguration<Adopter>
{
    public void Configure(EntityTypeBuilder<Adopter> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.FirstName).HasMaxLength(50);

        builder.Property(x => x.LastName).HasMaxLength(50);

        builder.OwnsOne(x => x.Email,
            email =>
            {
                email.Property(x => x.Value).IsRequired();
            });

        builder.OwnsOne(x => x.Phone,
            phone =>
            {
                phone.Property(x => x.CountryCode).IsRequired();
                phone.Property(x => x.Number).IsRequired();
            });

        builder.OwnsOne(x => x.Address,
            address =>
            {
                address.Property(x => x.Street).IsRequired();
                address.Property(x => x.City).IsRequired();
                address.Property(x => x.State).IsRequired();
                address.Property(x => x.PostalCode).IsRequired();
                address.Property(x => x.Country).IsRequired();
            });

        builder
            .HasMany(x => x.AdoptionRecords)
            .WithOne()
            .HasForeignKey(x => x.AdopterId);
    }
}
