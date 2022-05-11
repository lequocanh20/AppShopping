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
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorites");
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.HasOne(x => x.AppUser).WithMany(x => x.Favorites).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Product).WithMany(x => x.Favorites).HasForeignKey(x => x.ProductId);
        }
    }
}
