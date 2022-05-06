using AppShopping_Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Data.Configuration
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired().HasMaxLength(5);

            builder.Property(x => x.Count).IsRequired();

            builder.Property(x => x.Promotion).IsRequired();

            builder.Property(x => x.Describe).IsRequired().HasMaxLength(4000).HasColumnType("nvarchar");
        }
    }
}
