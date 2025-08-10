using Cargo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations
{
    /// <summary>
    /// EF Core configuration for the DriverBatchHourly entity.
    /// Configures hourly pay entries within a driver batch.
    /// </summary>
    public class DriverBatchHourlyConfiguration : IEntityTypeConfiguration<DriverBatchHourly>
    {
        public void Configure(EntityTypeBuilder<DriverBatchHourly> builder)
        {
            // Table mapping
            builder.ToTable("DriverBatchHourlies");

            // Primary key
            builder.HasKey(dh => dh.Id);

            // Relationship: DriverBatchHourly -> DriverBatch
            builder.HasOne(dh => dh.DriverBatch)
                   .WithMany(db => db.Hourlies)
                   .HasForeignKey(dh => dh.DriverBatchId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Properties
            builder.Property(dh => dh.Date)
                   .IsRequired();

            builder.Property(dh => dh.Hours)
                   .IsRequired();

            builder.Property(dh => dh.Minutes)
                   .IsRequired();

            builder.Property(dh => dh.RatePerHour)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(dh => dh.TotalPay)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            // Indexes
            builder.HasIndex(dh => dh.DriverBatchId);
        }
    }
}
