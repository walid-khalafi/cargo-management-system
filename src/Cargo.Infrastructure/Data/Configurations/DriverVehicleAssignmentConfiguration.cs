using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    /// <summary>
    /// EF Core configuration for the DriverVehicleAssignment entity.
    /// Configures driver-vehicle assignments with ownership details.
    /// </summary>
    public class DriverVehicleAssignmentConfiguration : IEntityTypeConfiguration<DriverVehicleAssignment>
    {
        public void Configure(EntityTypeBuilder<DriverVehicleAssignment> builder)
        {
            // Table mapping
            builder.ToTable("DriverVehicleAssignments");

            // Primary key
            builder.HasKey(dva => dva.Id);

            // Relationship: DriverVehicleAssignment -> Driver
            builder.HasOne(dva => dva.Driver)
                   .WithMany()
                   .HasForeignKey(dva => dva.DriverId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: DriverVehicleAssignment -> Vehicle
            builder.HasOne(dva => dva.Vehicle)
                   .WithMany()
                   .HasForeignKey(dva => dva.VehicleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(dva => dva.DriverRole)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(dva => dva.AssignedAt)
                   .IsRequired();

            builder.Property(dva => dva.EndedAt);

            builder.Property(dva => dva.EndReason)
                   .HasMaxLength(500);

            builder.Property(dva => dva.Status)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(dva => dva.Notes)
                   .HasMaxLength(1000);

            // Indexes
            builder.HasIndex(dva => new { dva.DriverId, dva.VehicleId, dva.AssignedAt })
                   .IsUnique();

            builder.HasIndex(dva => dva.DriverId);
            builder.HasIndex(dva => dva.VehicleId);
        }
    }
}
