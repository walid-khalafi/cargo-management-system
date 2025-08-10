using Cargo.Domain.Entities;
using Cargo.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    /// <summary>
    /// EF Core configuration for the DriverBatchWait entity.
    /// Configures waiting time entries within a driver batch.
    /// </summary>
    public class DriverBatchWaitConfiguration : IEntityTypeConfiguration<DriverBatchWait>
    {
        public void Configure(EntityTypeBuilder<DriverBatchWait> builder)
        {
            // Table mapping
            builder.ToTable("DriverBatchWaits");

            // Primary key
            builder.HasKey(dw => dw.Id);

            // Relationship: DriverBatchWait -> DriverBatch
            builder.HasOne(dw => dw.DriverBatch)
                   .WithMany(db => db.Waits)
                   .HasForeignKey(dw => dw.DriverBatchId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(dw => dw.DarNumber)
                   .HasMaxLength(50);

            builder.Property(dw => dw.CpPoNumber)
                   .HasMaxLength(50);

            builder.Property(dw => dw.WaitType)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(dw => dw.WaitMinutes)
                   .IsRequired();

            builder.Property(dw => dw.RatePerMinute)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(dw => dw.Multiplier)
                   .HasColumnType("decimal(5,2)")
                   .IsRequired();

            builder.Property(dw => dw.RawPay)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(dw => dw.FinalPay)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Indexes
            builder.HasIndex(dw => dw.DriverBatchId);
            builder.HasIndex(dw => dw.WaitType);
        }
    }
}
