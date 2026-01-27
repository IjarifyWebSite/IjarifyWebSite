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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(r => r.Comment)
                .HasColumnType("varchar")
                .HasMaxLength(500);
            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Review_Rating_Range", "Rating Between 1 and 10");
            });

            builder.HasOne(r => r.property)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.PropertyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r=>r.user)
                .WithMany(u=>u.reviews)
                .HasForeignKey(r=>r.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
