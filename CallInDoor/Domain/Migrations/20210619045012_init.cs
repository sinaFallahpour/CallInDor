using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CultureName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Bio = table.Column<string>(nullable: true),
                    ImageAddress = table.Column<string>(nullable: true),
                    VideoAddress = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    NationalCode = table.Column<string>(nullable: true),
                    BirthDate = table.Column<string>(maxLength: 40, nullable: true),
                    IsEditableProfile = table.Column<bool>(nullable: false),
                    ConnectionId = table.Column<string>(nullable: true),
                    CurentRequestId = table.Column<int>(nullable: false),
                    ChatNotificationId = table.Column<string>(nullable: true),
                    WalletBalance = table.Column<double>(nullable: true),
                    IsCompany = table.Column<bool>(nullable: false),
                    ProfileConfirmType = table.Column<int>(nullable: false),
                    IsOnline = table.Column<bool>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    verificationCode = table.Column<int>(nullable: true),
                    verificationCodeExpireTime = table.Column<DateTime>(nullable: false),
                    CountryCode = table.Column<string>(nullable: true),
                    StarCount = table.Column<int>(nullable: false),
                    Under3StarCount = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    CardName = table.Column<string>(nullable: true),
                    CardNumber = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTBL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    SenderUserName = table.Column<string>(nullable: true),
                    TextPersian = table.Column<string>(nullable: true),
                    EnglishText = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    IsReaded = table.Column<bool>(nullable: false),
                    NotificationStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTBL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    ActionName = table.Column<string>(maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    EnglishValue = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    order_status = table.Column<string>(nullable: true),
                    popularity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tiket",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    PriorityStatus = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    TiketStatus = table.Column<int>(nullable: false),
                    UserLastUpdateDate = table.Column<DateTime>(nullable: false),
                    AdminLastUpdateDate = table.Column<DateTime>(nullable: false),
                    IsUserSendNewMessgae = table.Column<bool>(nullable: false),
                    IsAdminSendNewMessgae = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PersianName = table.Column<string>(maxLength: 100, nullable: false),
                    ImageAddress = table.Column<string>(nullable: true),
                    Color = table.Column<string>(maxLength: 100, nullable: false),
                    IsProfileOptional = table.Column<bool>(nullable: false),
                    MinPriceForService = table.Column<double>(nullable: false),
                    MinSessionTime = table.Column<double>(nullable: false),
                    AcceptedMinPriceForNative = table.Column<double>(nullable: false),
                    AcceptedMinPriceForNonNative = table.Column<double>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    SitePercent = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false),
                    AppRoleId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_AppRoleId",
                        column: x => x.AppRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    DegreeType = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FirmProfile",
                columns: table => new
                {
                    AppUserId = table.Column<string>(nullable: false),
                    FirmName = table.Column<string>(maxLength: 240, nullable: true),
                    FirmManagerName = table.Column<string>(maxLength: 240, nullable: true),
                    FirmLogo = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(maxLength: 200, nullable: true),
                    CodePosti = table.Column<string>(maxLength: 200, nullable: true),
                    FirmAddress = table.Column<string>(nullable: true),
                    FirmCountry = table.Column<string>(maxLength: 200, nullable: true),
                    FirmState = table.Column<string>(maxLength: 200, nullable: true),
                    FirmNationalID = table.Column<string>(maxLength: 20, nullable: true),
                    FirmDateOfRegistration = table.Column<string>(maxLength: 30, nullable: true),
                    FirmRegistrationID = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmProfile", x => x.AppUserId);
                    table.ForeignKey(
                        name: "FK_FirmProfile_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWithdrawlRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    WithdrawlRequestStatus = table.Column<int>(nullable: false),
                    ResonOfReject = table.Column<string>(nullable: true),
                    RejectOrConfirmTime = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    CardItId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWithdrawlRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWithdrawlRequest_Card_CardItId",
                        column: x => x.CardItId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role_Permission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionId = table.Column<int>(nullable: true),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Permission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Role_Permission_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TiketMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsFile = table.Column<bool>(nullable: false),
                    FileAddress = table.Column<string>(nullable: true),
                    TiketId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiketMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiketMessages_Tiket_TiketId",
                        column: x => x.TiketId,
                        principalTable: "Tiket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    PersianTitle = table.Column<string>(maxLength: 120, nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsProfessional = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Area_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    PersianTitle = table.Column<string>(maxLength: 200, nullable: true),
                    ImageAddress = table.Column<string>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsForCourse = table.Column<bool>(nullable: false),
                    IsSubCategory = table.Column<bool>(nullable: false),
                    IsSupplier = table.Column<bool>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersianTitle = table.Column<string>(nullable: true),
                    EnglishTitle = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Percent = table.Column<int>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    DayCount = table.Column<int>(nullable: true),
                    HourCount = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckDiscount_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyServiceUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTimeOffset>(nullable: false),
                    ConfirmStatus = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    subSetUserName = table.Column<string>(nullable: true),
                    CompanyUserName = table.Column<string>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyServiceUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyServiceUser_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionPull",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    EnglishText = table.Column<string>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionPull", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionPull_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(nullable: true),
                    PersianTagName = table.Column<string>(nullable: true),
                    IsEnglisTags = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceTags_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServidceTypeRequiredCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    PersianFileName = table.Column<string>(nullable: true),
                    Isdeleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServidceTypeRequiredCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServidceTypeRequiredCertificates_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopTenPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    DayCount = table.Column<int>(nullable: true),
                    HourCount = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopTenPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopTenPackage_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FirmServiceCategoryInterInterFace",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTBLId = table.Column<int>(nullable: true),
                    FirmProfileTBLId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmServiceCategoryInterInterFace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirmServiceCategoryInterInterFace_FirmProfile_FirmProfileTBLId",
                        column: x => x.FirmProfileTBLId,
                        principalTable: "FirmProfile",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FirmServiceCategoryInterInterFace_Service_ServiceTBLId",
                        column: x => x.ServiceTBLId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersianName = table.Column<string>(nullable: true),
                    EnglishName = table.Column<string>(nullable: true),
                    AreatId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Speciality_Area_AreatId",
                        column: x => x.AreatId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BaseMyService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(maxLength: 200, nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    IsProfileOptional = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    IsEditableService = table.Column<bool>(nullable: false),
                    ConfirmedServiceType = table.Column<int>(nullable: false),
                    RejectReason = table.Column<string>(nullable: true),
                    ProfileConfirmType = table.Column<int>(nullable: false),
                    ProfileRejectReson = table.Column<string>(nullable: true),
                    ServiceTypes = table.Column<string>(nullable: true),
                    ServiceType = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsDisabledByCompany = table.Column<bool>(nullable: false),
                    StarCount = table.Column<int>(nullable: false),
                    Under3StarCount = table.Column<int>(nullable: false),
                    CompanyId = table.Column<string>(nullable: true),
                    CatId = table.Column<int>(nullable: true),
                    SubCatId = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMyService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseMyService_Category_CatId",
                        column: x => x.CatId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseMyService_AspNetUsers_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseMyService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseMyService_Category_SubCatId",
                        column: x => x.SubCatId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    EnglishText = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_QuestionPull_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionPull",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfileCertificate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    FileAddress = table.Column<string>(maxLength: 2000, nullable: true),
                    ProfileConfirmType = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true),
                    RequiredCertificatesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileCertificate_ServidceTypeRequiredCertificates_RequiredCertificatesId",
                        column: x => x.RequiredCertificatesId,
                        principalTable: "ServidceTypeRequiredCertificates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileCertificate_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User-TopTenPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    TopTenPackageId = table.Column<int>(nullable: true),
                    ServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User-TopTenPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User-TopTenPackage_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User-TopTenPackage_TopTenPackage_TopTenPackageId",
                        column: x => x.TopTenPackageId,
                        principalTable: "TopTenPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BaseServiceRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienUserName = table.Column<string>(nullable: true),
                    ProvideUserName = table.Column<string>(nullable: true),
                    ServiceTypes = table.Column<string>(nullable: true),
                    ServiceRequestStatus = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastTimeTheMessageWasSent = table.Column<DateTime>(nullable: false),
                    PackageType = table.Column<int>(nullable: false),
                    WhenTheRequestShouldBeAnswered = table.Column<DateTime>(nullable: false),
                    PriceForNonNativeCustomer = table.Column<double>(nullable: true),
                    PriceForNativeCustomer = table.Column<double>(nullable: true),
                    BaseServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseServiceRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseServiceRequest_BaseMyService_BaseServiceId",
                        column: x => x.BaseServiceId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MyChatService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageType = table.Column<int>(nullable: true),
                    BeTranslate = table.Column<bool>(nullable: false),
                    FreeMessageCount = table.Column<int>(nullable: true),
                    Duration = table.Column<int>(nullable: true),
                    MessageCount = table.Column<int>(nullable: true),
                    IsServiceReverse = table.Column<bool>(nullable: false),
                    PriceForNativeCustomer = table.Column<double>(nullable: true),
                    PriceForNonNativeCustomer = table.Column<double>(nullable: true),
                    BaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyChatService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyChatService_BaseMyService_BaseId",
                        column: x => x.BaseId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyCourseService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    NewCategory = table.Column<string>(maxLength: 200, nullable: true),
                    TotalLenght = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    DisCountPercent = table.Column<int>(nullable: true),
                    PreviewVideoAddress = table.Column<string>(nullable: true),
                    BaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCourseService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyCourseService_BaseMyService_BaseId",
                        column: x => x.BaseId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MyServiceService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    BeTranslate = table.Column<bool>(nullable: false),
                    FileNeeded = table.Column<bool>(nullable: false),
                    FileDescription = table.Column<string>(maxLength: 600, nullable: true),
                    Price = table.Column<double>(nullable: true),
                    WorkDeliveryTimeEstimation = table.Column<string>(maxLength: 600, nullable: true),
                    HowWorkConducts = table.Column<string>(maxLength: 600, nullable: true),
                    DeliveryItems = table.Column<string>(maxLength: 600, nullable: true),
                    Tags = table.Column<string>(maxLength: 600, nullable: true),
                    AreaId = table.Column<int>(nullable: true),
                    SpecialityId = table.Column<int>(nullable: true),
                    BaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyServiceService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyServiceService_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyServiceService_BaseMyService_BaseId",
                        column: x => x.BaseId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyServiceService_Speciality_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Speciality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 120, nullable: true),
                    Comment = table.Column<string>(maxLength: 400, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ResonForUnder3Star = table.Column<string>(maxLength: 400, nullable: true),
                    StarCount = table.Column<int>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    BaseMyServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceComments_BaseMyService_BaseMyServiceId",
                        column: x => x.BaseMyServiceId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceSurvey",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 120, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true),
                    AnswerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceSurvey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceSurvey_Answer_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceSurvey_QuestionPull_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionPull",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceSurvey_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    TransactionStatus = table.Column<int>(nullable: false),
                    TransactionType = table.Column<int>(nullable: false),
                    ServiceTypeWithDetails = table.Column<string>(nullable: true),
                    ProviderUserName = table.Column<string>(nullable: true),
                    ClientUserName = table.Column<string>(nullable: true),
                    BaseMyServiceId = table.Column<int>(nullable: true),
                    TransactionConfirmedStatus = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: true),
                    CheckDiscountId = table.Column<int>(nullable: true),
                    User_TopTenPackageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_BaseMyService_BaseMyServiceId",
                        column: x => x.BaseMyServiceId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_CheckDiscount_CheckDiscountId",
                        column: x => x.CheckDiscountId,
                        principalTable: "CheckDiscount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_User-TopTenPackage_User_TopTenPackageId",
                        column: x => x.User_TopTenPackageId,
                        principalTable: "User-TopTenPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CallRequestTBL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    RealEndTime = table.Column<DateTime>(nullable: false),
                    BaseRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallRequestTBL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CallRequestTBL_BaseServiceRequest_BaseRequestId",
                        column: x => x.BaseRequestId,
                        principalTable: "BaseServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MyCourseTopics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(maxLength: 200, nullable: true),
                    IsFreeForEveryOne = table.Column<bool>(nullable: false),
                    FileAddress = table.Column<string>(nullable: true),
                    IsConfirmByAdmin = table.Column<bool>(nullable: false),
                    MyCourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCourseTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyCourseTopics_MyCourseService_MyCourseId",
                        column: x => x.MyCourseId,
                        principalTable: "MyCourseService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienUserName = table.Column<string>(nullable: true),
                    ProvideUserName = table.Column<string>(nullable: true),
                    ServiceTypes = table.Column<string>(nullable: true),
                    ServiceRequestStatus = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastTimeTheMessageWasSent = table.Column<DateTime>(nullable: false),
                    FreeUsageMessageCount = table.Column<int>(nullable: true),
                    FreeMessageCount = table.Column<int>(nullable: true),
                    PackageType = table.Column<int>(nullable: false),
                    WhenTheRequestShouldBeAnswered = table.Column<DateTime>(nullable: false),
                    PriceForNonNativeCustomer = table.Column<double>(nullable: true),
                    PriceForNativeCustomer = table.Column<double>(nullable: true),
                    AllMessageCount_LimitedChat = table.Column<int>(nullable: false),
                    UsedMessageCount_LimitedChat = table.Column<int>(nullable: false),
                    IsLimitedChat = table.Column<bool>(nullable: false),
                    HasPlan_LimitedChatVoice = table.Column<bool>(nullable: false),
                    ExpireTime_LimitedChatVoice = table.Column<DateTime>(nullable: false),
                    BuyiedPackageId = table.Column<int>(nullable: true),
                    BaseServiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_BaseMyService_BaseServiceId",
                        column: x => x.BaseServiceId,
                        principalTable: "BaseMyService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuyiedPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    BuyiedPackageStatus = table.Column<int>(nullable: false),
                    MainPrice = table.Column<double>(nullable: true),
                    FinalPrice = table.Column<double>(nullable: true),
                    SitePercent = table.Column<int>(nullable: false),
                    BuyiedPackageType = table.Column<int>(nullable: false),
                    IsRenewPackage = table.Column<bool>(nullable: false),
                    MessgaeCount = table.Column<int>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CheckDiscountId = table.Column<int>(nullable: true),
                    ServiceRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyiedPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyiedPackage_CheckDiscount_CheckDiscountId",
                        column: x => x.CheckDiscountId,
                        principalTable: "CheckDiscount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuyiedPackage_ServiceRequest_ServiceRequestId",
                        column: x => x.ServiceRequestId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatRequest",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FreeUsageMessageCount = table.Column<int>(nullable: true),
                    FreeMessageCount = table.Column<int>(nullable: true),
                    PackageType = table.Column<int>(nullable: false),
                    WhenTheRequestShouldBeAnswered = table.Column<DateTime>(nullable: false),
                    PriceForNonNativeCustomer = table.Column<double>(nullable: true),
                    PriceForNativeCustomer = table.Column<double>(nullable: true),
                    AllMessageCount_LimitedChat = table.Column<int>(nullable: false),
                    UsedMessageCount_LimitedChat = table.Column<int>(nullable: false),
                    IsLimitedChat = table.Column<bool>(nullable: false),
                    HasPlan_LimitedChatVoice = table.Column<bool>(nullable: false),
                    ExpireTime_LimitedChatVoice = table.Column<DateTime>(nullable: false),
                    BuyiedPackageId = table.Column<int>(nullable: true),
                    BaseRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRequest_BaseServiceRequest_BaseRequestId",
                        column: x => x.BaseRequestId,
                        principalTable: "BaseServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatRequest_BuyiedPackage_BuyiedPackageId",
                        column: x => x.BuyiedPackageId,
                        principalTable: "BuyiedPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatForLimitedServiceMessagesTBL",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderUserName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ChatMessageType = table.Column<int>(nullable: false),
                    FileOrVoiceAddress = table.Column<string>(nullable: true),
                    ProviderUserName = table.Column<string>(maxLength: 100, nullable: true),
                    ClientUserName = table.Column<string>(maxLength: 100, nullable: true),
                    IsProviderSend = table.Column<bool>(nullable: false),
                    SendetMesageType = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false),
                    BaseServiceRequestId = table.Column<int>(nullable: true),
                    ChatRequestId = table.Column<int>(nullable: true),
                    ServiceRequestTBLId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatForLimitedServiceMessagesTBL", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatForLimitedServiceMessagesTBL_BaseServiceRequest_BaseServiceRequestId",
                        column: x => x.BaseServiceRequestId,
                        principalTable: "BaseServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatForLimitedServiceMessagesTBL_ChatRequest_ChatRequestId",
                        column: x => x.ChatRequestId,
                        principalTable: "ChatRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatForLimitedServiceMessagesTBL_ServiceRequest_ServiceRequestTBLId",
                        column: x => x.ServiceRequestTBLId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatServiceMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderUserName = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    ChatMessageType = table.Column<int>(nullable: false),
                    FileOrVoiceAddress = table.Column<string>(nullable: true),
                    ProviderUserName = table.Column<string>(maxLength: 40, nullable: true),
                    ClientUserName = table.Column<string>(maxLength: 40, nullable: true),
                    IsProviderSend = table.Column<bool>(nullable: false),
                    SendetMesageType = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: true),
                    IsSeen = table.Column<bool>(nullable: false),
                    BaseServiceRequestId = table.Column<int>(nullable: true),
                    ChatRequestId = table.Column<int>(nullable: true),
                    ServiceRequestTBLId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatServiceMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatServiceMessages_BaseServiceRequest_BaseServiceRequestId",
                        column: x => x.BaseServiceRequestId,
                        principalTable: "BaseServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatServiceMessages_ChatRequest_ChatRequestId",
                        column: x => x.ChatRequestId,
                        principalTable: "ChatRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatServiceMessages_ServiceRequest_ServiceRequestTBLId",
                        column: x => x.ServiceRequestTBLId,
                        principalTable: "ServiceRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Area_ServiceId",
                table: "Area",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_AppRoleId",
                table: "AspNetUserRoles",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_AppUserId",
                table: "AspNetUserRoles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SerialNumber",
                table: "AspNetUsers",
                column: "SerialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_CatId",
                table: "BaseMyService",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_CompanyId",
                table: "BaseMyService",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_ServiceId",
                table: "BaseMyService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_ServiceName",
                table: "BaseMyService",
                column: "ServiceName");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_ServiceType",
                table: "BaseMyService",
                column: "ServiceType");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_SubCatId",
                table: "BaseMyService",
                column: "SubCatId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMyService_ServiceName_ServiceType",
                table: "BaseMyService",
                columns: new[] { "ServiceName", "ServiceType" });

            migrationBuilder.CreateIndex(
                name: "IX_BaseServiceRequest_BaseServiceId",
                table: "BaseServiceRequest",
                column: "BaseServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyiedPackage_CheckDiscountId",
                table: "BuyiedPackage",
                column: "CheckDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyiedPackage_ServiceRequestId",
                table: "BuyiedPackage",
                column: "ServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CallRequestTBL_BaseRequestId",
                table: "CallRequestTBL",
                column: "BaseRequestId",
                unique: true,
                filter: "[BaseRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ServiceId",
                table: "Category",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatForLimitedServiceMessagesTBL_BaseServiceRequestId",
                table: "ChatForLimitedServiceMessagesTBL",
                column: "BaseServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatForLimitedServiceMessagesTBL_ChatRequestId",
                table: "ChatForLimitedServiceMessagesTBL",
                column: "ChatRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatForLimitedServiceMessagesTBL_ServiceRequestTBLId",
                table: "ChatForLimitedServiceMessagesTBL",
                column: "ServiceRequestTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRequest_BaseRequestId",
                table: "ChatRequest",
                column: "BaseRequestId",
                unique: true,
                filter: "[BaseRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRequest_BuyiedPackageId",
                table: "ChatRequest",
                column: "BuyiedPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatServiceMessages_BaseServiceRequestId",
                table: "ChatServiceMessages",
                column: "BaseServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatServiceMessages_ChatRequestId",
                table: "ChatServiceMessages",
                column: "ChatRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatServiceMessages_ServiceRequestTBLId",
                table: "ChatServiceMessages",
                column: "ServiceRequestTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckDiscount_ServiceId",
                table: "CheckDiscount",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyServiceUser_ServiceId",
                table: "CompanyServiceUser",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_UserId",
                table: "Field",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmServiceCategoryInterInterFace_FirmProfileTBLId",
                table: "FirmServiceCategoryInterInterFace",
                column: "FirmProfileTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmServiceCategoryInterInterFace_ServiceTBLId",
                table: "FirmServiceCategoryInterInterFace",
                column: "ServiceTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_BaseId",
                table: "MyChatService",
                column: "BaseId",
                unique: true,
                filter: "[BaseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MyChatService_PriceForNativeCustomer_PriceForNonNativeCustomer",
                table: "MyChatService",
                columns: new[] { "PriceForNativeCustomer", "PriceForNonNativeCustomer" });

            migrationBuilder.CreateIndex(
                name: "IX_MyCourseService_BaseId",
                table: "MyCourseService",
                column: "BaseId",
                unique: true,
                filter: "[BaseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MyCourseTopics_MyCourseId",
                table: "MyCourseTopics",
                column: "MyCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_AreaId",
                table: "MyServiceService",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_BaseId",
                table: "MyServiceService",
                column: "BaseId",
                unique: true,
                filter: "[BaseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MyServiceService_SpecialityId",
                table: "MyServiceService",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCertificate_RequiredCertificatesId",
                table: "ProfileCertificate",
                column: "RequiredCertificatesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileCertificate_ServiceId",
                table: "ProfileCertificate",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionPull_ServiceId",
                table: "QuestionPull",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Permission_PermissionId",
                table: "Role_Permission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Permission_RoleId",
                table: "Role_Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_RoleId",
                table: "Service",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceComments_BaseMyServiceId",
                table: "ServiceComments",
                column: "BaseMyServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_BaseServiceId",
                table: "ServiceRequest",
                column: "BaseServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_BuyiedPackageId",
                table: "ServiceRequest",
                column: "BuyiedPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSurvey_AnswerId",
                table: "ServiceSurvey",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSurvey_QuestionId",
                table: "ServiceSurvey",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceSurvey_ServiceId",
                table: "ServiceSurvey",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTags_ServiceId",
                table: "ServiceTags",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServidceTypeRequiredCertificates_ServiceId",
                table: "ServidceTypeRequiredCertificates",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Speciality_AreatId",
                table: "Speciality",
                column: "AreatId");

            migrationBuilder.CreateIndex(
                name: "IX_TiketMessages_TiketId",
                table: "TiketMessages",
                column: "TiketId");

            migrationBuilder.CreateIndex(
                name: "IX_TopTenPackage_ServiceId",
                table: "TopTenPackage",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BaseMyServiceId",
                table: "Transaction",
                column: "BaseMyServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CardId",
                table: "Transaction",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CheckDiscountId",
                table: "Transaction",
                column: "CheckDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_User_TopTenPackageId",
                table: "Transaction",
                column: "User_TopTenPackageId",
                unique: true,
                filter: "[User_TopTenPackageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User-TopTenPackage_ServiceId",
                table: "User-TopTenPackage",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_User-TopTenPackage_TopTenPackageId",
                table: "User-TopTenPackage",
                column: "TopTenPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWithdrawlRequest_CardItId",
                table: "UserWithdrawlRequest",
                column: "CardItId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequest_BuyiedPackage_BuyiedPackageId",
                table: "ServiceRequest",
                column: "BuyiedPackageId",
                principalTable: "BuyiedPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMyService_Service_ServiceId",
                table: "BaseMyService");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Service_ServiceId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckDiscount_Service_ServiceId",
                table: "CheckDiscount");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseMyService_AspNetUsers_CompanyId",
                table: "BaseMyService");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseMyService_Category_CatId",
                table: "BaseMyService");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseMyService_Category_SubCatId",
                table: "BaseMyService");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequest_BaseMyService_BaseServiceId",
                table: "ServiceRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyiedPackage_CheckDiscount_CheckDiscountId",
                table: "BuyiedPackage");

            migrationBuilder.DropForeignKey(
                name: "FK_BuyiedPackage_ServiceRequest_ServiceRequestId",
                table: "BuyiedPackage");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CallRequestTBL");

            migrationBuilder.DropTable(
                name: "ChatForLimitedServiceMessagesTBL");

            migrationBuilder.DropTable(
                name: "ChatServiceMessages");

            migrationBuilder.DropTable(
                name: "CompanyServiceUser");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "FirmServiceCategoryInterInterFace");

            migrationBuilder.DropTable(
                name: "MyChatService");

            migrationBuilder.DropTable(
                name: "MyCourseTopics");

            migrationBuilder.DropTable(
                name: "MyServiceService");

            migrationBuilder.DropTable(
                name: "NotificationTBL");

            migrationBuilder.DropTable(
                name: "ProfileCertificate");

            migrationBuilder.DropTable(
                name: "Role_Permission");

            migrationBuilder.DropTable(
                name: "ServiceComments");

            migrationBuilder.DropTable(
                name: "ServiceSurvey");

            migrationBuilder.DropTable(
                name: "ServiceTags");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "TiketMessages");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "UserWithdrawlRequest");

            migrationBuilder.DropTable(
                name: "ChatRequest");

            migrationBuilder.DropTable(
                name: "FirmProfile");

            migrationBuilder.DropTable(
                name: "MyCourseService");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropTable(
                name: "ServidceTypeRequiredCertificates");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Tiket");

            migrationBuilder.DropTable(
                name: "User-TopTenPackage");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "BaseServiceRequest");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "QuestionPull");

            migrationBuilder.DropTable(
                name: "TopTenPackage");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "BaseMyService");

            migrationBuilder.DropTable(
                name: "CheckDiscount");

            migrationBuilder.DropTable(
                name: "ServiceRequest");

            migrationBuilder.DropTable(
                name: "BuyiedPackage");
        }
    }
}
