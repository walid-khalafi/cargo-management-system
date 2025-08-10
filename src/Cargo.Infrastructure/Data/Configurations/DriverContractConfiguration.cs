using Cargo.Domain.Entities;
using Cargo.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Infrastructure.Data.Configurations
{
    /// <summary>
    /// EF Core configuration for the DriverContract aggregate.
    /// Configures owned value objects, relationships, keys, and constraints.
    /// </summary>

    public class DriverContractConfiguration : IEntityTypeConfiguration<DriverContract>

    {
        public void Configure(EntityTypeBuilder<DriverContract> builder)
        {
            // Table mapping
            builder.ToTable("DriverContracts");

            // Primary key
            builder.HasKey(dc => dc.Id);

            // Relationship: DriverContract -> Driver
            builder.HasOne(dc => dc.Driver)
                   .WithMany() // Or WithMany(d => d.Contracts) if navigation is defined in Driver
                   .HasForeignKey(dc => dc.DriverId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Owned single value object: DriverSettings
            builder.OwnsOne(dc => dc.Settings, settings =>
            {
                settings.Property(s => s.NumPayBands)
                        .HasColumnName("NumPayBands")
                        .IsRequired();

                settings.Property(s => s.HourlyRate)
                        .HasColumnName("HourlyRate")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();

                settings.Property(s => s.FscRate)
                        .HasColumnName("FscRate")
                        .HasColumnType("decimal(5,2)")
                        .IsRequired();

                settings.Property(s => s.FscMode)
                        .HasColumnName("FscMode")
                        .HasConversion<int>()
                        .IsRequired();

                settings.Property(s => s.WaitingPerMinute)
                        .HasColumnName("WaitingPerMinute")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();

                settings.Property(s => s.AdminFee)
                        .HasColumnName("AdminFee")
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();

                settings.Property(s => s.Province)
                        .HasColumnName("Province")
                        .HasMaxLength(50)
                        .IsRequired();

                settings.OwnsOne(s => s.TaxProfile, taxProfile =>
                {
                    taxProfile.Property(t => t.GstRate)
                             .HasColumnName("GstRate")
                             .HasColumnType("decimal(5,2)")
                             .IsRequired();
                    
                    taxProfile.Property(t => t.QstRate)
                             .HasColumnName("QstRate")
                             .HasColumnType("decimal(5,2)")
                             .IsRequired();
                    
                    taxProfile.Property(t => t.PstRate)
                             .HasColumnName("PstRate")
                             .HasColumnType("decimal(5,2)")
                             .IsRequired();
                    
                    taxProfile.Property(t => t.HstRate)
                             .HasColumnName("HstRate")
                             .HasColumnType("decimal(5,2)")
                             .IsRequired();
                    
                    taxProfile.Property(t => t.CompoundQstOverGst)
                             .HasColumnName("CompoundQstOverGst")
                             .IsRequired();
                });
            });

            // Owned collection value object: RateBands
            builder.OwnsMany(dc => dc.RateBands, rb =>
            {
                rb.ToTable("RateBands");

                // Foreign key to owning DriverContract
                rb.WithOwner().HasForeignKey("DriverContractId");

                // Composite key to prevent duplicates within a contract
                rb.HasKey("DriverContractId", nameof(RateBand.Band), nameof(RateBand.LoadType));

                // Property mappings
                rb.Property(r => r.Band).ValueGeneratedNever();

                rb.Property(r => r.MinMiles)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();

                rb.Property(r => r.MaxMiles)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();

                rb.Property(r => r.BandName)
                  .HasMaxLength(50)
                  .IsRequired();

                // Enum conversion for LoadType
                rb.Property(r => r.LoadType)
                  .HasConversion<int>()
                  .IsRequired();

                rb.Property(r => r.ContainerRate)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();

                rb.Property(r => r.FlatbedRate)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();
            });

            // Ensure EF uses the backing field for RateBands
            builder.Metadata.FindNavigation(nameof(DriverContract.RateBands))!
                   .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Dates
            builder.Property(dc => dc.StartDate).IsRequired();
            builder.Property(dc => dc.EndDate);
        }

    }
}
