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
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.TotalPrice)
                .HasPrecision(10, 2);
            builder.Ignore(b => b.IsValid);

            builder.HasOne(b => b.Property)
                .WithMany(b => b.Bookings)
                .HasForeignKey(b => b.PropertyID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.user)
               .WithMany(u => u.Bookings)
               .HasForeignKey(b=>b.UsertID)
               .OnDelete(DeleteBehavior.NoAction);

            builder.Property(b => b.Status)
                .HasConversion<string>();



        }
    }

}
