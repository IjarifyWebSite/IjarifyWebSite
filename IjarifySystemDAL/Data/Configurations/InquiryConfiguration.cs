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
    public class InquiryConfiguration : IEntityTypeConfiguration<Inquiry>
    {
        public void Configure(EntityTypeBuilder<Inquiry> builder)
        {
            builder.ToTable("Inquiries").HasKey(i => i.Id);

            builder.Property(i => i.Message).HasColumnType("varchar").HasMaxLength(800);
            builder.Property<DateTime>("CreatedAt").HasDefaultValueSql("GETDATE()");

            builder.HasOne(i => i.User)
                   .WithMany(u => u.UserInquiries)
                   .HasForeignKey(i => i.UserId);

            builder.HasOne(i => i.Property)
                   .WithMany(p => p.PropertyInquiries)
                   .HasForeignKey(i => i.PropertyId);
        }
    }
}
