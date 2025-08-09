using Cargo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.FirstName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(d => d.LastName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(d => d.Email)
            .IsRequired()
            .HasMaxLength(256);
            
        builder.Property(d => d.PhoneNumber)
            .HasMaxLength(20);
            
        builder.Property(d => d.LicenseNumber)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(d => d.LicenseType)
            .HasMaxLength(20);
            
        // Configure owned types
        builder.OwnsOne(d => d.Address, address =>
        {
            address.Property(a => a.Street).HasColumnName("Address_Street").HasMaxLength(200);
            address.Property(a => a.City).HasColumnName("Address_City").HasMaxLength(100);
            address.Property(a => a.State).HasColumnName("Address_State").HasMaxLength(50);
            address.Property(a => a.ZipCode).HasColumnName("Address_ZipCode").HasMaxLength(20);
            address.Property(a => a.Country).HasColumnName("Address_Country").HasMaxLength(50);
        });
        
        // Configure relationships
        builder.HasOne(d => d.Company)
            .WithMany(c => c.Drivers)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Indexes
        builder.HasIndex(d => d.Email).IsUnique();
        builder.HasIndex(d => d.LicenseNumber).IsUnique();
    }
}
