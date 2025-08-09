using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");
        
        builder.HasKey(v => v.Id);
        
        builder.Property(v => v.Make)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(v => v.Model)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(v => v.VIN)
            .IsRequired()
            .HasMaxLength(17);
            
        builder.Property(v => v.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(v => v.Color)
            .HasMaxLength(50);
            
        builder.Property(v => v.FuelType)
            .HasMaxLength(50);
            
        builder.Property(v => v.CurrentLocation)
            .HasMaxLength(200);
            
        // Configure owned types
        builder.OwnsOne(v => v.PlateNumber, plateNumber =>
        {
            plateNumber.Property(p => p.Value).HasColumnName("PlateNumber_Value").HasMaxLength(20);
            plateNumber.Property(p => p.IssuingAuthority).HasColumnName("PlateNumber_IssuingAuthority").HasMaxLength(50);
        });
        
        // Configure relationships
        builder.HasOne(v => v.OwnerCompany)
            .WithMany()
            .HasForeignKey(v => v.OwnerCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Indexes
        builder.HasIndex(v => v.VIN).IsUnique();
        builder.HasIndex(v => v.RegistrationNumber).IsUnique();
        builder.HasIndex(v => v.OwnerCompanyId).IsUnique();
        builder.HasIndex(v => v.DriverId).IsUnique();
    }
}
