﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Project.Tereza.Infrastructure.DBContext;

#nullable disable

namespace Project.Tereza.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220427194028_AddIdentityAndSeedRolesUsers")]
    partial class AddIdentityAndSeedRolesUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Project.Tereza.Core.Entities.Identity.AppRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AppRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("0a34f5d9-2211-42ef-80a0-65d9f24dc67e"),
                            ConcurrencyStamp = "0a34f5d9-2211-42ef-80a0-65d9f24dc67e",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN",
                            RoleDescription = "May manage Admins, Highest permissions"
                        },
                        new
                        {
                            Id = new Guid("53afebc1-3d10-4b5f-a8c7-a4f4a8628bc1"),
                            ConcurrencyStamp = "53afebc1-3d10-4b5f-a8c7-a4f4a8628bc1",
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            RoleDescription = "Ordinary Admin"
                        },
                        new
                        {
                            Id = new Guid("7faec745-6e30-4428-ba6a-7f1140146ff2"),
                            ConcurrencyStamp = "7faec745-6e30-4428-ba6a-7f1140146ff2",
                            Name = "User",
                            NormalizedName = "USER",
                            RoleDescription = "Tipical user"
                        });
                });

            modelBuilder.Entity("Project.Tereza.Core.Entities.Identity.AppUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AppUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5bb520a6-02bc-4dc0-b805-e5c66783898a"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "5bb520a6-02bc-4dc0-b805-e5c66783898a",
                            Email = "superadmin@projecttereza.org",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            Name = "SuperAdmin",
                            NormalizedEmail = "SUPERADMIN@PROJECTTEREZA.ORG",
                            NormalizedUserName = "SUPERADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEGE+LGK3sVpq1x8dp5WUJWQC6Jy/LIzhf2C0h38cJsqiuNkH+boBPUBstf8/t1BvBg==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "R2QKAFY6HJCWISKITA7JHISY2B4ISNIZ",
                            Surname = "SuperAdmin",
                            TwoFactorEnabled = false,
                            UserName = "SuperAdmin"
                        },
                        new
                        {
                            Id = new Guid("f14bb510-b4fb-4e8b-bd99-e5426f55c8f8"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f14bb510-b4fb-4e8b-bd99-e5426f55c8f8",
                            Email = "testuser@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = true,
                            Name = "TestUser",
                            NormalizedEmail = "TESTUSER@GMAIL.COM",
                            NormalizedUserName = "TESTUSER",
                            PasswordHash = "AQAAAAEAACcQAAAAEJj2DDiwxfh3IkBUdzzcLy8oFkjr7uYEFQn8JpdaXPB65GYTJtW9sLrFY6X8/BAL+g==",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "PULC6TBDHH3ZKN7Q4CNWX4QRPKI7ZQKR",
                            Surname = "TestUser",
                            TwoFactorEnabled = false,
                            UserName = "TestUser"
                        });
                });

            modelBuilder.Entity("Project.Tereza.Core.Entities.Identity.AppUserRoles", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AppUsersRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("5bb520a6-02bc-4dc0-b805-e5c66783898a"),
                            RoleId = new Guid("0a34f5d9-2211-42ef-80a0-65d9f24dc67e")
                        },
                        new
                        {
                            UserId = new Guid("f14bb510-b4fb-4e8b-bd99-e5426f55c8f8"),
                            RoleId = new Guid("7faec745-6e30-4428-ba6a-7f1140146ff2")
                        });
                });

            modelBuilder.Entity("Project.Tereza.Core.Need", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsCovered")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Needs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("82d257a5-d72b-4f08-bcf2-76ebdc958b5f"),
                            Count = 1,
                            Description = "Need laptop for working needs.",
                            IsCovered = false,
                            Name = "Laptop"
                        },
                        new
                        {
                            Id = new Guid("b47fdb0a-76d4-4b89-bf20-9cecfa4f4f82"),
                            Count = 1,
                            Description = "I need food for my cat, please help!",
                            IsCovered = false,
                            Name = "Royal Canin Sphyncx 2 kg"
                        },
                        new
                        {
                            Id = new Guid("a961067e-c777-42c2-8fee-71180d750bd7"),
                            Count = 3,
                            Description = "Please, I can't find this drug in retail",
                            IsCovered = false,
                            Name = "Aspirin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Project.Tereza.Core.Entities.Identity.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Project.Tereza.Core.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Project.Tereza.Core.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Project.Tereza.Core.Entities.Identity.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Project.Tereza.Core.Entities.Identity.AppUserRoles", b =>
                {
                    b.HasOne("Project.Tereza.Core.Entities.Identity.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Tereza.Core.Entities.Identity.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project.Tereza.Core.Entities.Identity.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Project.Tereza.Core.Entities.Identity.AppUser", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
