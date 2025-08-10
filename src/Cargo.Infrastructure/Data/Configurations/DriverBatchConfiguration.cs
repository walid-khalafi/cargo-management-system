using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    /// <summary>
    /// EF Core configuration for the DriverBatch aggregate.
    /// Configures batch processing for driver activities.
    /// </summary>
    public class DriverBatchConfiguration : IEntityTypeConfiguration<DriverBatch>
    {
        public void Configure(EntityTypeBuilder<DriverBatch> builder)
        {
            // Table mapping
            builder.ToTable("DriverBatches");

            // Primary key
            builder.HasKey(db => db.Id);

            // Relationship: DriverBatch -> Driver
            builder.HasOne(db => db.Driver)
                   .WithMany()
                   .HasForeignKey(db => db.DriverId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relationship: DriverBatch -> VehicleOwnership (optional)
            builder.HasOne(db => db.VehicleOwnership)
                   .WithMany()
                   .HasForeignKey(db => db.VehicleOwnershipId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Properties
            builder.Property(db => db.BatchNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(db => db.StatementStartDate)
                   .IsRequired();

            builder.Property(db => db.StatementEndDate)
                   .IsRequired();

            builder.Property(db => db.OwnershipTypeAtBatch)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(db => db.DrayPay)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(db => db.DrayFsc)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(db => db.TemporaryEmergencyFsc)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(db => db.WaitingPayoutPercentage)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(db => db.DriverSharePercentage)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(db => db.AdminFeePercent)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(db => db.AdminFeeFlat)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(db => db.AdminFeeAppliesBeforeTaxes)
                   .IsRequired();

            builder.Property(db => db.AdjustmentsTotal)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(db => db.Status)
                   .IsRequired()
                   .HasConversion<int>();

            // Configure owned types
            builder.OwnsOne(db => db.TaxProfile, taxProfile =>
            {
                taxProfile.Property(t => t.GstRate).HasColumnName("TaxProfile_GstRate").HasPrecision(5, 4);
                taxProfile.Property(t => t.QstRate).HasColumnName("TaxProfile_QstRate").HasPrecision(5, 4);
                taxProfile.Property(t => t.PstRate).HasColumnName("TaxProfile_PstRate").HasPrecision(5, 4);
                taxProfile.Property(t => t.HstRate).HasColumnName("TaxProfile_HstRate").HasPrecision(5, 4);
                taxProfile.Property(t => t.CompoundQstOverGst).HasColumnName("TaxProfile_CompoundQstOverGst");
            });

            builder.OwnsOne(db => db.Taxes, taxes =>
            {
                taxes.Property(t => t.GstAmount).HasColumnName("Taxes_GstAmount").HasColumnType("decimal(18,2)");
                taxes.Property(t => t.QstAmount).HasColumnName("Taxes_QstAmount").HasColumnType("decimal(18,2)");
                taxes.Property(t => t.PstAmount).HasColumnName("Taxes_PstAmount").HasColumnType("decimal(18,2)");
                taxes.Property(t => t.HstAmount).HasColumnName("Taxes_HstAmount").HasColumnType("decimal(18,2)");
            });

            // Indexes
            builder.HasIndex(db => db.BatchNumber)
                   .IsUnique();

            builder.HasIndex(db => db.DriverId);
            builder.HasIndex(db => db.Status);
            builder.HasIndex(db => db.StatementStartDate);
            builder.HasIndex(db => db.StatementEndDate);
        }
    }
}
