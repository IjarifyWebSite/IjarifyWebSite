using IjarifySystemDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Data.Configurations
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations").HasKey(l => l.Id);

            builder.Property(l => l.City).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(l => l.Regoin).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(l => l.Street).HasColumnType("varchar").HasMaxLength(50);

            builder.HasMany(l => l.Properties)
                   .WithOne(p => p.Location)
                   .HasForeignKey(p => p.LocationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
