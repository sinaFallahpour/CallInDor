﻿// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201020173332_add-role-permissin")]
    partial class addrolepermissin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Domain.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImageAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int?>("verificationCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("verificationCodeExpireTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Domain.Entities.AreaTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsProfessional")
                        .HasColumnType("bit");

                    b.Property<string>("PersianTitle")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("Domain.Entities.BaseMyServiceTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CatId")
                        .HasColumnType("int");

                    b.Property<int>("ConfirmedServiceType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int>("ServiceType")
                        .HasColumnType("int");

                    b.Property<int?>("SubCatId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CatId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("SubCatId");

                    b.ToTable("BaseMyService");
                });

            modelBuilder.Entity("Domain.Entities.CategoryTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsForCourse")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSubCategory")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("PersianTitle")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Domain.Entities.FieldTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DegreeType")
                        .HasColumnType("int");

                    b.Property<string>("PersianTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("Domain.Entities.MyChatServiceTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BaseId")
                        .HasColumnType("int");

                    b.Property<bool>("BeTranslate")
                        .HasColumnType("bit");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("FreeMessageCount")
                        .HasColumnType("int");

                    b.Property<bool>("IsServiceReverse")
                        .HasColumnType("bit");

                    b.Property<int?>("PackageType")
                        .HasColumnType("int");

                    b.Property<double>("PriceForNativeCustomer")
                        .HasColumnType("float");

                    b.Property<double>("PriceForNonNativeCustomer")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BaseId")
                        .IsUnique()
                        .HasFilter("[BaseId] IS NOT NULL");

                    b.ToTable("MyChatService");
                });

            modelBuilder.Entity("Domain.Entities.MyCourseServiceTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BaseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int>("DisCountPercent")
                        .HasColumnType("int");

                    b.Property<string>("NewCategory")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("PreviewVideoAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("TotalLenght")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BaseId")
                        .IsUnique()
                        .HasFilter("[BaseId] IS NOT NULL");

                    b.ToTable("MyCourseService");
                });

            modelBuilder.Entity("Domain.Entities.MyCourseTopics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConfirmByAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFreeForEveryOne")
                        .HasColumnType("bit");

                    b.Property<int?>("MyCourseId")
                        .HasColumnType("int");

                    b.Property<string>("TopicName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("MyCourseId");

                    b.ToTable("MyCourseTopics");
                });

            modelBuilder.Entity("Domain.Entities.MyServiceServiceTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int?>("BaseId")
                        .HasColumnType("int");

                    b.Property<bool>("BeTranslate")
                        .HasColumnType("bit");

                    b.Property<string>("DeliveryItems")
                        .HasColumnType("nvarchar(600)")
                        .HasMaxLength(600);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("FileDescription")
                        .HasColumnType("nvarchar(600)")
                        .HasMaxLength(600);

                    b.Property<bool>("FileNeeded")
                        .HasColumnType("bit");

                    b.Property<string>("HowWorkConducts")
                        .HasColumnType("nvarchar(600)")
                        .HasMaxLength(600);

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("SpecialityId")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(600)")
                        .HasMaxLength(600);

                    b.Property<string>("WorkDeliveryTimeEstimation")
                        .HasColumnType("nvarchar(600)")
                        .HasMaxLength(600);

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("BaseId")
                        .IsUnique()
                        .HasFilter("[BaseId] IS NOT NULL");

                    b.HasIndex("SpecialityId");

                    b.ToTable("MyServiceService");
                });

            modelBuilder.Entity("Domain.Entities.Permissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActionName")
                        .HasColumnType("nvarchar(400)")
                        .HasMaxLength(400);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Domain.Entities.Role_Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PermissionId")
                        .HasColumnType("int");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("Role_Permission");
                });

            modelBuilder.Entity("Domain.Entities.ServiceTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AcceptedMinPriceForNative")
                        .HasColumnType("float");

                    b.Property<double>("AcceptedMinPriceForNonNative")
                        .HasColumnType("float");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<double>("MinPriceForService")
                        .HasColumnType("float");

                    b.Property<double>("MinSessionTime")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("PersianName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("Domain.Entities.ServiceTagsTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnglisTags")
                        .HasColumnType("bit");

                    b.Property<string>("PersianTagName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int");

                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceTags");
                });

            modelBuilder.Entity("Domain.Entities.SpecialityTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AreatId")
                        .HasColumnType("int");

                    b.Property<string>("EnglishName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersianName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreatId");

                    b.ToTable("Speciality");
                });

            modelBuilder.Entity("Domain.Entities.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("order_status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("popularity")
                        .HasColumnType("int");

                    b.Property<double>("price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Domain.Entities.User_FieldTBL", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FieldId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("UserId");

                    b.ToTable("User_Field");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Domain.Entities.AreaTBL", b =>
                {
                    b.HasOne("Domain.Entities.ServiceTBL", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("Domain.Entities.BaseMyServiceTBL", b =>
                {
                    b.HasOne("Domain.Entities.CategoryTBL", "CategoryTBL")
                        .WithMany()
                        .HasForeignKey("CatId");

                    b.HasOne("Domain.Entities.ServiceTBL", "ServiceTbl")
                        .WithMany("BaseMyServices")
                        .HasForeignKey("ServiceId");

                    b.HasOne("Domain.Entities.CategoryTBL", "SubCategoryTBL")
                        .WithMany()
                        .HasForeignKey("SubCatId");
                });

            modelBuilder.Entity("Domain.Entities.CategoryTBL", b =>
                {
                    b.HasOne("Domain.Entities.CategoryTBL", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("Domain.Entities.ServiceTBL", "Service")
                        .WithMany("Categories")
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("Domain.Entities.MyChatServiceTBL", b =>
                {
                    b.HasOne("Domain.Entities.BaseMyServiceTBL", "BaseMyChatTBL")
                        .WithOne("MyChatsService")
                        .HasForeignKey("Domain.Entities.MyChatServiceTBL", "BaseId");
                });

            modelBuilder.Entity("Domain.Entities.MyCourseServiceTBL", b =>
                {
                    b.HasOne("Domain.Entities.BaseMyServiceTBL", "BaseMyChatTBL")
                        .WithOne("MyCourseService")
                        .HasForeignKey("Domain.Entities.MyCourseServiceTBL", "BaseId");
                });

            modelBuilder.Entity("Domain.Entities.MyCourseTopics", b =>
                {
                    b.HasOne("Domain.Entities.MyCourseServiceTBL", "MyCourseServiceTBL")
                        .WithMany("TopicsTBLs")
                        .HasForeignKey("MyCourseId");
                });

            modelBuilder.Entity("Domain.Entities.MyServiceServiceTBL", b =>
                {
                    b.HasOne("Domain.Entities.AreaTBL", "AreaTBL")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("Domain.Entities.BaseMyServiceTBL", "BaseMyChatTBL")
                        .WithOne("MyServicesService")
                        .HasForeignKey("Domain.Entities.MyServiceServiceTBL", "BaseId");

                    b.HasOne("Domain.Entities.SpecialityTBL", "SpecialityTBL")
                        .WithMany()
                        .HasForeignKey("SpecialityId");
                });

            modelBuilder.Entity("Domain.Entities.Role_Permission", b =>
                {
                    b.HasOne("Domain.Entities.Permissions", "Permissions")
                        .WithMany("Role_Permissions")
                        .HasForeignKey("PermissionId");

                    b.HasOne("Domain.Entities.AppRole", "AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Domain.Entities.ServiceTBL", b =>
                {
                    b.HasOne("Domain.Entities.AppRole", "AppRole")
                        .WithMany("Services")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Domain.Entities.ServiceTagsTBL", b =>
                {
                    b.HasOne("Domain.Entities.ServiceTBL", "Service")
                        .WithMany("Tags")
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("Domain.Entities.SpecialityTBL", b =>
                {
                    b.HasOne("Domain.Entities.AreaTBL", "Area")
                        .WithMany("Specialities")
                        .HasForeignKey("AreatId");
                });

            modelBuilder.Entity("Domain.Entities.User_FieldTBL", b =>
                {
                    b.HasOne("Domain.Entities.FieldTBL", "FieldTBL")
                        .WithMany()
                        .HasForeignKey("FieldId");

                    b.HasOne("Domain.Entities.AppUser", "User")
                        .WithMany("UsersFields")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Domain.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Domain.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domain.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
