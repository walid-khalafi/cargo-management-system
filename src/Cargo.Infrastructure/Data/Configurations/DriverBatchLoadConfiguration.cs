using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    /// <summary>
    /// EF Core configuration for the DriverBatchLoad entity.
    /// Configures individual load entries within a driver batch.
    /// </summary>
    public class DriverBatchLoadConfiguration : IEntityTypeConfiguration<DriverBatchLoad>
    {
        public void Configure(EntityTypeBuilder<DriverBatchLoad> builder)
        {
            // Table mapping
            builder.ToTable("DriverBatchLoads");

            // Primary key
            builder.HasKey(dl => dl.Id);

            // Relationship: DriverBatchLoad -> DriverBatch
            builder.HasOne(dl => dl.DriverBatch)
                   .WithMany(db => db.Loads)
                   .HasForeignKey(dl => dl.DriverBatchId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(dl => dl.DarNumber)
                   .HasMaxLength(50);

            builder.Property(dl => dl.LoadNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(dl => dl.OriginPc)
                   .HasMaxLength(10);

            builder.Property(dl => dl.DestinationPc)
                   .HasMaxLength(10);

            builder.Property(dl => dl.LegMiles)
                   .IsRequired();

            builder.Property(dl => dl.LoadType)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(dl => dl.RateType)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(dl => dl.Rate)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(dl => dl.BandLabel)
                   .HasMaxLength(50);

            builder.Property(dl => dl.BasePay)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(dl => dl.FscPay)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(dl => dl.TemporaryEmergencyFuelPay)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(dl => dl.NetWefp)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Indexes
            builder.HasIndex(dl => dl.DriverBatchId);
            builder.HasIndex(dl => dl.LoadNumber);
            builder.HasIndex(dl => dl.LoadType);
        }
    }
}
