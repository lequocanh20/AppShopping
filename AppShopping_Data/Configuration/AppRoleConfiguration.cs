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
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            // do AppRole đã có id do class IdentityUser chỉ định nên không cần
            // xác định id cho Entity này

            builder.ToTable("AppRoles");

            builder.Property(x => x.Id).HasMaxLength(200);
        }
    }
}
