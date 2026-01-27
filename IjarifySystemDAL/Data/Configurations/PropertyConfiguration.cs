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
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties").HasKey(p => p.Id);
            builder.Property(p => p.Title).HasColumnType("varchar").HasMaxLength(200);
            builder.Property(p => p.Description).HasColumnType("varchar").HasMaxLength(800);
            builder.Property<DateTime>("CreatedAt").HasDefaultValueSql("GETDATE()");

            builder.HasMany(p => p.PropertyImages)
                   .WithOne(pi => pi.Property)
                   .HasForeignKey(pi => pi.PropertyId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.amenities)
                .WithMany(p => p.properties);

            builder.Property(p=>p.ListingType)
              .HasConversion<string>();

            builder.Property(p=>p.Type)
              .HasConversion<string>();

        }
    }
}
