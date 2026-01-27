using IjarifySystemDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace IjarifySystemDAL.Data.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offers").HasKey(o => o.Id);
            builder.Property(o => o.Title).HasColumnType("varchar").HasMaxLength(200);
            builder.Property(o => o.DiscountPercentage).HasPrecision(5, 2);
            builder.Property(x => x.CreatedAt).HasColumnName("StartDate").HasDefaultValueSql("GETDATE()");

            builder.HasOne(o => o.Property)
                   .WithMany(p => p.PropertyOffers)
                   .HasForeignKey(o => o.PropertyId);
        }
    }
}
