using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    /// <summary>
    /// EF Core configuration for the VehicleOwnership entity.
    /// Configures vehicle ownership records with company relationships.
    /// </summary>
    public class VehicleOwnershipConfiguration : IEntityTypeConfiguration<VehicleOwnership>
    {
        public void Configure(EntityTypeBuilder<VehicleOwnership> builder)
        {
            // Table mapping
            builder.ToTable("VehicleOwnerships");

            // Primary key
            builder.HasKey(vo => vo.Id);

            // Relationship: VehicleOwnership -> Vehicle
            builder.HasOne(vo => vo.Vehicle)
                   .WithMany()
                   .HasForeignKey(vo => vo.VehicleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: VehicleOwnership -> Company
            builder.HasOne(vo => vo.OwnerCompany)
                   .WithMany()
                   .HasForeignKey(vo => vo.OwnerCompanyId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(vo => vo.VehicleId)
                   .IsRequired();

            builder.Property(vo => vo.OwnerCompanyId)
                   .IsRequired();

            builder.Property(vo => vo.Type)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(vo => vo.OwnedFrom)
                   .IsRequired();

            builder.Property(vo => vo.OwnedUntil);

            // Indexes
            builder.HasIndex(vo => new { vo.VehicleId, vo.OwnerCompanyId, vo.OwnedFrom })
                   .IsUnique();

            builder.HasIndex(vo => vo.VehicleId);
            builder.HasIndex(vo => vo.OwnerCompanyId);
        }
    }
}
