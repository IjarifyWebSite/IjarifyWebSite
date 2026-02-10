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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(u => u.Id);

            builder.Property(u => u.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(u => u.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(u => u.Phone).HasColumnType("varchar").HasMaxLength(11);
            builder.Property(u => u.Address).HasColumnType("varchar").HasMaxLength(100);
            builder.Property<DateTime>("CreatedAt").HasDefaultValueSql("GETDATE()");


            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("CK_IjarifyUserValidEmail", "Email like '_%@_%._%'");
                tb.HasCheckConstraint("CK_IjarifyUserValidPhone", "phone like '01%' and Phone not like '%[^0-9]%'");
            });

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();

            builder.HasMany(u => u.Properties)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
