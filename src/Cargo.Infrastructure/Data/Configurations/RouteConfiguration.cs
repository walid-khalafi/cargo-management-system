using Cargo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cargo.Infrastructure.Data.Configurations;

public class RouteConfiguration : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
        builder.ToTable("Routes");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(r => r.Description)
            .HasMaxLength(1000);
            
        builder.Property(r => r.Origin)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(r => r.TotalDistance)
            .HasPrecision(10, 2);
            
        builder.Property(r => r.EstimatedDuration)
            .HasPrecision(5, 2);
            
       
    }
}
