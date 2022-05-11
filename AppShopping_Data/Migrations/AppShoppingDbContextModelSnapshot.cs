﻿// <auto-generated />
using System;
using AppShopping_Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppShopping_Data.Migrations
{
    [DbContext(typeof(AppShoppingDbContext))]
    partial class AppShoppingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppShopping_Data.Entities.AppConfig", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key");

                    b.ToTable("AppConfigs");

                    b.HasData(
                        new
                        {
                            Key = "HomeTitle",
                            Value = "This is home page of App Shopping"
                        },
                        new
                        {
                            Key = "HomeKeyword",
                            Value = "This is keyword of App Shopping"
                        },
                        new
                        {
                            Key = "HomeDescription",
                            Value = "This is description of App Shopping"
                        });
                });

            modelBuilder.Entity("AppShopping_Data.Entities.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                            ConcurrencyStamp = "7cdba651-c5f5-4ad6-8194-088f23eb03e5",
                            Description = "Administrator role",
                            Name = "admin",
                            NormalizedName = "admin"
                        });
                });

            modelBuilder.Entity("AppShopping_Data.Entities.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            AccessFailedCount = 0,
                            Address = "123 Lien Ap 2-6 X.Vinh Loc A H. Binh Chanh",
                            ConcurrencyStamp = "ebfa8e94-1664-43e9-8bdd-3b7b62b71c29",
                            Email = "lequocanh.qa@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            Name = "Quoc Anh",
                            NormalizedEmail = "lequocanh.qa@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEASYOP/grh4CrEsXGy8Nb2vEkJDxzNur9ddYA5/ZQ+CJdU2ljXfgscQONc+sQxw6LQ==",
                            PhoneNumber = "0774642207",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageCate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageCate = "assets/images/dw.png",
                            Name = "Daniel Wellington"
                        },
                        new
                        {
                            Id = 2,
                            ImageCate = "assets/images/casio.png",
                            Name = "Casio"
                        },
                        new
                        {
                            Id = 3,
                            ImageCate = "assets/images/citizen.png",
                            Name = "Citizen"
                        },
                        new
                        {
                            Id = 4,
                            ImageCate = "assets/images/seiko.png",
                            Name = "Seiko"
                        },
                        new
                        {
                            Id = 5,
                            ImageCate = "assets/images/orient.png",
                            Name = "Orient"
                        });
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Describe")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<int>("Promotion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Favorite", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CouponId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CouponId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasMaxLength(100000000)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductImage")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Stock")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<decimal>("originPrice")
                        .HasMaxLength(100000000)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            DateCreated = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Đồng hồ mang thương hiệu Daniel Wellington",
                            Name = "DANIEL WELLINGTON DW00100414",
                            Price = 6600000m,
                            ProductImage = "assets/foods/ic_black_coffee.png",
                            Stock = 5,
                            originPrice = 6000000m
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            DateCreated = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Đồng hồ mang thương hiệu Casio",
                            Name = "CASIO EFB-302JD-1ADR",
                            Price = 10882000m,
                            ProductImage = "assets/foods/ic_black_coffee.png",
                            Stock = 5,
                            originPrice = 10000000m
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            DateCreated = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Đồng hồ mang thương hiệu Citizen",
                            Name = "CITIZEN NB1021-57E",
                            Price = 14700000m,
                            ProductImage = "assets/foods/ic_black_coffee.png",
                            Stock = 5,
                            originPrice = 14000000m
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 4,
                            DateCreated = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Đồng hồ mang thương hiệu Seiko",
                            Name = "SEIKO SSB361P1",
                            Price = 6625000m,
                            ProductImage = "assets/foods/ic_black_coffee.png",
                            Stock = 5,
                            originPrice = 6000000m
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 5,
                            DateCreated = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Đồng hồ mang thương hiệu Orient",
                            Name = "ORIENT RA-AR0001S10B",
                            Price = 10170000m,
                            ProductImage = "assets/foods/ic_black_coffee.png",
                            Stock = 5,
                            originPrice = 9000000m
                        });
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Comments = "Xịn",
                            ProductId = 1,
                            PublishedDate = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rating = 5.0,
                            Status = 1,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de")
                        },
                        new
                        {
                            Id = 2,
                            Comments = "Xịn",
                            ProductId = 2,
                            PublishedDate = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rating = 5.0,
                            Status = 1,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de")
                        },
                        new
                        {
                            Id = 3,
                            Comments = "Xịn",
                            ProductId = 3,
                            PublishedDate = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rating = 5.0,
                            Status = 1,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de")
                        },
                        new
                        {
                            Id = 4,
                            Comments = "Xịn",
                            ProductId = 4,
                            PublishedDate = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rating = 5.0,
                            Status = 1,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de")
                        },
                        new
                        {
                            Id = 5,
                            Comments = "Xịn",
                            ProductId = 5,
                            PublishedDate = new DateTime(2021, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Rating = 5.0,
                            Status = 1,
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                            RoleId = new Guid("8d04dce2-969a-435d-bba4-df3f325983dc")
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Favorite", b =>
                {
                    b.HasOne("AppShopping_Data.Entities.Product", "Product")
                        .WithMany("Favorites")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppShopping_Data.Entities.AppUser", "AppUser")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Order", b =>
                {
                    b.HasOne("AppShopping_Data.Entities.Coupon", "Coupon")
                        .WithMany("Orders")
                        .HasForeignKey("CouponId");

                    b.HasOne("AppShopping_Data.Entities.AppUser", "AppUser")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");

                    b.Navigation("Coupon");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.OrderDetail", b =>
                {
                    b.HasOne("AppShopping_Data.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppShopping_Data.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Product", b =>
                {
                    b.HasOne("AppShopping_Data.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Review", b =>
                {
                    b.HasOne("AppShopping_Data.Entities.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppShopping_Data.Entities.AppUser", "AppUser")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.AppUser", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Coupon", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("AppShopping_Data.Entities.Product", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("OrderDetails");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
