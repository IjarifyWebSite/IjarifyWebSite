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
    internal class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.Property(c => c.Category).HasColumnType("varchar").HasMaxLength(30);
            builder.Property(a => a.Name)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(a => a.Icon)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(a => a.Category)
                .HasConversion<string>()
                .HasColumnType("varchar");

        }
    }
}
