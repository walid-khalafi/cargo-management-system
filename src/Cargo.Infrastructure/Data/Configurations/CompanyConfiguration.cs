using Cargo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(c => c.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(50);
            
        // Configure owned types
        builder.OwnsOne(c => c.Address, address =>
        {
            address.Property(a => a.Street).HasColumnName("Address_Street").HasMaxLength(200);
            address.Property(a => a.City).HasColumnName("Address_City").HasMaxLength(100);
            address.Property(a => a.State).HasColumnName("Address_State").HasMaxLength(50);
            address.Property(a => a.ZipCode).HasColumnName("Address_ZipCode").HasMaxLength(20);
            address.Property(a => a.Country).HasColumnName("Address_Country").HasMaxLength(50);
        });
        
        builder.OwnsOne(c => c.TaxProfile, taxProfile =>
        {
            taxProfile.Property(t => t.GstRate).HasColumnName("TaxProfile_GstRate").HasPrecision(5, 4);
            taxProfile.Property(t => t.QstRate).HasColumnName("TaxProfile_QstRate").HasPrecision(5, 4);
            taxProfile.Property(t => t.PstRate).HasColumnName("TaxProfile_PstRate").HasPrecision(5, 4);
            taxProfile.Property(t => t.HstRate).HasColumnName("TaxProfile_HstRate").HasPrecision(5, 4);
            taxProfile.Property(t => t.CompoundQstOverGst).HasColumnName("TaxProfile_CompoundQstOverGst");
        });
        
        // Configure relationships
        builder.HasMany(c => c.Drivers)
            .WithOne(d => d.Company)
            .HasForeignKey(d => d.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
            
        builder.HasMany(c => c.Vehicles)
            .WithOne(v => v.OwnerCompany)
            .HasForeignKey(v => v.OwnerCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
