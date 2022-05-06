using AppShopping_Data.Entities;
using AppShopping_Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppShopping_Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of App Shopping" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of App Shopping" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of App Shopping" }
               );

            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            builder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "lequocanh.qa@gmail.com",
                NormalizedEmail = "lequocanh.qa@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                PhoneNumber = "0774642207",
                Address = "123 Lien Ap 2-6 X.Vinh Loc A H. Binh Chanh",
                Name = "Quoc Anh",
            });

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            #region Seed Category
            builder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    Name = "Daniel Wellington",
                    ImageCate = "assets/images/dw.png"
                },

                new Category()
                {
                    Id = 2,
                    Name = "Casio",
                    ImageCate = "assets/images/casio.png"
                },

                new Category()
                {
                    Id = 3,
                    Name = "Citizen",
                    ImageCate = "assets/images/citizen.png"
                },

                new Category()
                {
                    Id = 4,
                    Name = "Seiko",
                    ImageCate = "assets/images/seiko.png"
                },

                new Category()
                {
                    Id = 5,
                    Name = "Orient",
                    ImageCate = "assets/images/orient.png"
                }
              );
            #endregion

            #region Seed Product
            builder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Name = "DANIEL WELLINGTON DW00100414",
                    CategoryId = 1,
                    originPrice = 6000000,
                    Price = 6600000,
                    Stock = 5,
                    Description = "Đồng hồ mang thương hiệu Daniel Wellington",
                    DateCreated = new DateTime(2021, 11, 18),
                    ProductImage = "assets/foods/ic_black_coffee.png",



                },

                new Product()
                {
                    Id = 2,
                    Name = "CASIO EFB-302JD-1ADR",
                    CategoryId = 2,
                    originPrice = 10000000,
                    Price = 10882000,
                    Stock = 5,
                    DateCreated = new DateTime(2021, 11, 18),
                    Description = "Đồng hồ mang thương hiệu Casio",
                    ProductImage = "assets/foods/ic_black_coffee.png"
                },

                new Product()
                {
                    Id = 3,
                    Name = "CITIZEN NB1021-57E",
                    CategoryId = 3,
                    originPrice = 14000000,
                    Price = 14700000,
                    Stock = 5,
                    DateCreated = new DateTime(2021, 11, 18),
                    Description = "Đồng hồ mang thương hiệu Citizen",
                    ProductImage = "assets/foods/ic_black_coffee.png"
                },

                new Product()
                {
                    Id = 4,
                    Name = "SEIKO SSB361P1",
                    CategoryId = 4,
                    originPrice = 6000000,
                    Price = 6625000,
                    Stock = 5,
                    DateCreated = new DateTime(2021, 11, 18),
                    Description = "Đồng hồ mang thương hiệu Seiko",
                    ProductImage = "assets/foods/ic_black_coffee.png"
                },

                new Product()
                {
                    Id = 5,
                    Name = "ORIENT RA-AR0001S10B",
                    CategoryId = 5,
                    originPrice = 9000000,
                    Price = 10170000,
                    Stock = 5,
                    DateCreated = new DateTime(2021, 11, 18),
                    Description = "Đồng hồ mang thương hiệu Orient",
                    ProductImage = "assets/foods/ic_black_coffee.png"
                }
              );
            #endregion

            #region Seed Review
            builder.Entity<Review>().HasData(
                new Review()
                {
                    Id = 1,
                    UserId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    ProductId = 1,
                    Rating = 5,
                    Comments = "Xịn",
                    PublishedDate = new DateTime(2021, 11, 18),
                    Status = (Status)1,
                },

                new Review()
                {
                    Id = 2,
                    UserId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    ProductId = 2,
                    Rating = 5,
                    Comments = "Xịn",
                    PublishedDate = new DateTime(2021, 11, 18),
                    Status = (Status)1,
                },

                new Review()
                {
                    Id = 3,
                    UserId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    ProductId = 3,
                    Rating = 5,
                    Comments = "Xịn",
                    PublishedDate = new DateTime(2021, 11, 18),
                    Status = (Status)1,
                },

                new Review()
                {
                    Id = 4,
                    UserId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    ProductId = 4,
                    Rating = 5,
                    Comments = "Xịn",
                    PublishedDate = new DateTime(2021, 11, 18),
                    Status = (Status)1,
                },

                new Review()
                {
                    Id = 5,
                    UserId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE"),
                    ProductId = 5,
                    Rating = 5,
                    Comments = "Xịn",
                    PublishedDate = new DateTime(2021, 11, 18),
                    Status = (Status)1,
                }
              );
            #endregion
        }
    }
}
