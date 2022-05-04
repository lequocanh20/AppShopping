using AppShopping_Data.Entities;
using AppShopping_Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Data.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Comments).IsRequired().HasMaxLength(500).HasColumnType("nvarchar");

            builder.Property(x => x.Rating).IsRequired().HasColumnType("float");

            builder.Property(x => x.Status).IsRequired().HasDefaultValue((Status)0);

            builder.HasOne(x => x.Product).WithMany(x => x.Reviews).HasForeignKey(x => x.ProductId);

            builder.HasOne(x => x.AppUser).WithMany(x => x.Reviews).HasForeignKey(x => x.UserId);
        }
    }
}
