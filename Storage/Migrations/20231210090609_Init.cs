using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Storage.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViTri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TinhThanhPho = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViTri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUserCustomer = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_AspNetUsers_IdUserCustomer",
                        column: x => x.IdUserCustomer,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DichVu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdDanhMuc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DichVu_DanhMuc_IdDanhMuc",
                        column: x => x.IdDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiNhanh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChiNhanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdViTri = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiNhanh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiNhanh_ViTri_IdViTri",
                        column: x => x.IdViTri,
                        principalTable: "ViTri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Barber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameBarber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdChiNhanhWork = table.Column<int>(type: "int", nullable: false),
                    IdUserBarber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Barber_AspNetUsers_IdUserBarber",
                        column: x => x.IdUserBarber,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Barber_ChiNhanh_IdChiNhanhWork",
                        column: x => x.IdChiNhanhWork,
                        principalTable: "ChiNhanh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    IdBarber = table.Column<int>(type: "int", nullable: false),
                    IdDichVu = table.Column<int>(type: "int", nullable: false),
                    IdChiNhanh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Booking_Barber_IdBarber",
                        column: x => x.IdBarber,
                        principalTable: "Barber",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_ChiNhanh_IdChiNhanh",
                        column: x => x.IdChiNhanh,
                        principalTable: "ChiNhanh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_Customer_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_DichVu_IdDichVu",
                        column: x => x.IdDichVu,
                        principalTable: "DichVu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HuyLichHen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LyDoHuyLich = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianHuy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBooking = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HuyLichHen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HuyLichHen_Booking_IdBooking",
                        column: x => x.IdBooking,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichHen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrangThaiHoanThanh = table.Column<bool>(type: "bit", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBooking = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichHen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LichHen_Booking_IdBooking",
                        column: x => x.IdBooking,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "1", "Admin", "Admin" },
                    { "2", "1", "Barber", "Barber" },
                    { "3", "1", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "8946c167-5836-4707-9060-290b015b9a53", "IdentityUser", "Admin@admin.com", true, false, null, "Admin@admin.com", "Admin", "AQAAAAIAAYagAAAAEPb2i0bOt0BT7vfmNJjLp3mGDaci4KJNpywxuNGwtqHk82f29BobabEraI4XpAhmpA==", null, false, "", false, "Admin" },
                    { "2", 0, "ebdd9cce-bcac-4eeb-9513-7bfe80ae4092", "IdentityUser", "Barber1@barber.com", true, false, null, "Barber1@barber.com", "Barber1", "AQAAAAIAAYagAAAAEE0vInjc8sOArbjDr0M3KwrCRMIrNNsHgjF2vcFywTIXAf1RWc2mc8RuE6ZRkQEtgQ==", null, false, "", false, "Barber1" },
                    { "3", 0, "8e939e45-da36-461b-8e23-c4c4dba7e76e", "IdentityUser", "Barber2@barber.com", true, false, null, "Barber2@barber.com", "Barber2", "AQAAAAIAAYagAAAAEAugHH5m6hBCCAeoIpZWpf/2hp/uCtrM3BCV8Sz9rpvUHEFosvGPulUO5D9RDC8dWg==", null, false, "", false, "Barber2" }
                });

            migrationBuilder.InsertData(
                table: "DanhMuc",
                columns: new[] { "Id", "MoTaDanhMuc", "TenDanhMuc" },
                values: new object[,]
                {
                    { 1, "Bao gồm các dịch vụ cắt gội massage thư giản", "Cắt Tóc" },
                    { 2, "Các dịch vụ uốn tóc cao cấp", "Uốn Tóc" },
                    { 3, "Duỗi thẳng tóc xoăn tóc gãy", "Duỗi Tóc" },
                    { 4, "Các dịch vụ nhuộm tóc cao cấp", "Nhuộm Tóc" },
                    { 5, "Phục hồi tóc bị hư tổn", "Phục Hồi Tóc" }
                });

            migrationBuilder.InsertData(
                table: "ViTri",
                columns: new[] { "Id", "TinhThanhPho" },
                values: new object[,]
                {
                    { 1, "Hồ Chí Minh" },
                    { 2, "Hà Nội" },
                    { 3, "Hải Phòng" },
                    { 4, "Bình Phước" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" },
                    { "2", "3" }
                });

            migrationBuilder.InsertData(
                table: "ChiNhanh",
                columns: new[] { "Id", "DiaChi", "IdViTri", "TenChiNhanh" },
                values: new object[,]
                {
                    { 1, "502 Đỗ Xuân Hợp, Phước Long B, TP. Thủ Đức", 1, "Barber Đỗ Xuân Hợp" },
                    { 2, "302 Lê Văn Việt, Phường Hiệp Phú, TP. Thủ Đức", 1, "Barber Lê Văn Việt" },
                    { 3, "113 Lê Thị Riêng, Bến Thành, Quận 1, TP.Hồ Chí Minh", 1, "Barber Lê Thị Riêng" },
                    { 4, "339 Lê Văn Sỹ, Phường 13, Quận 3, TP. Hồ Chí Minh", 1, "Barber Lê Văn Sỹ" },
                    { 5, "Số 5 Tuệ Tĩnh, Hai Bà Trưng, Hà Nội", 2, "Barber Tuệ Tĩnh" },
                    { 6, "Số 38A Triệu Việt Vương, Hai Bà Trưng, Hà Nội", 2, "Barber Triệu Việt Vương" },
                    { 7, "Số 37 Phạm Hồng Thái, Trúc Bạch, Ba Đình, Hà Nội", 2, "Barber Phạm Hồng Thái" },
                    { 8, "Số 30 Hạ Hồi, Hoàn Kiếm, Hà Nội", 2, "Barber Hạ Hồi" },
                    { 9, "Số 20 Đường Đông Khê 1, Ngô Quyền, Hải Phòng", 3, "Barber Đông Khê" },
                    { 10, "Số 86 Hải Triều, Quán Toan, Hải Phòng", 3, "Barber Hải Triều" },
                    { 11, "24 Lê Đại Hành, Hồng Bàng, Hải Phòng", 3, "Barber Lê Đại Hành" },
                    { 12, "100 Lạch Tray, Ngô Quyền, Hải Phòng", 3, "Barber Lạch Tray" },
                    { 13, "722 Phú Riềng Đỏ, P Tân Xuân, Tp Đồng Xoài, Bình Phước", 4, "Barber Phú Riềng Đỏ" },
                    { 14, "Đường Nguyễn Huệ, Kp3, P. Tân Đồng, TX. Đồng Xoài, Bình Phước", 4, "Barber Nguyễn Huệ" },
                    { 15, "87 Hùng Vương, Tân Bình, Đồng Xoài, Bình Phước", 4, "Barber Hùng Vương" }
                });

            migrationBuilder.InsertData(
                table: "DichVu",
                columns: new[] { "Id", "Gia", "IdDanhMuc", "MoTa", "TenDichVu" },
                values: new object[,]
                {
                    { 1, 60000m, 1, "Tạo kiểu mái phù hợp với khuôn mặt và phong cách cá nhân", "Cắt tóc mái" },
                    { 2, 120000m, 1, "Cắt tóc để tạo ra độ dài và kiểu dáng mong muốn trên toàn bộ tóc", "Cắt tóc đều" },
                    { 3, 150000m, 1, "Sấy và làm tóc xoăn hoặc làm thẳng", "Cắt tóc tạo kiểu" },
                    { 4, 320000m, 2, "Sấy tóc để tạo kiểu xoăn hoặc thẳng nhanh chóng", "Uốn tóc nhanh" },
                    { 5, 700000m, 2, "Sử dụng hóa chất để tạo kiểu xoăn tóc kéo dài thời gian", "Uốn tóc lâu dài" },
                    { 6, 720000m, 2, "Tạo kiểu tóc với những sóng tự nhiên giống như kiểu tóc sau khi đi biển", "Uốn tóc xù" },
                    { 7, 500000m, 3, "Sử dụng sản phẩm chứa keratin để giảm độ xoăn và tăng độ mềm mại cho tóc", "Duỗi tóc keratin" },
                    { 8, 500000m, 3, "Sử dụng máy laze để làm thẳng tóc", "Duỗi tóc máy laze" },
                    { 9, 500000m, 3, "Sử dụng hơi nước và thiết bị ion để làm thẳng tóc", "Duỗi tóc ion hơi nước" },
                    { 10, 500000m, 3, "Sử dụng hóa chất để làm thẳng tóc và giữ độ thẳng lâu dài", "Duỗi tóc ổn định" },
                    { 11, 900000m, 4, "Thay đổi màu sắc của toàn bộ bộ tóc", "Nhuộm toàn bộ tóc" },
                    { 12, 500000m, 4, "Nhuộm chỉ một phần cụ thể của tóc, như mái, đuôi, hoặc mèo", "Nhuộm phần tóc" },
                    { 13, 950000m, 4, "Tạo những sợi tóc nhấn màu, thường là những sợi tóc màu sáng hơn so với màu tự nhiên của tóc", "Highlight" },
                    { 14, 800000m, 5, "Sử dụng dầu nhiệt đới để làm nóng tóc và tăng cường dưỡng chất cho tóc", " Phục hồi bằng dầu nhiệt đới" },
                    { 15, 820000m, 5, "Cho những người đã nhuộm tóc, dịch vụ này giúp bảo quản và tái tạo màu sắc của tóc", "Phục hồi màu tóc" }
                });

            migrationBuilder.InsertData(
                table: "Barber",
                columns: new[] { "Id", "IdChiNhanhWork", "IdUserBarber", "NameBarber" },
                values: new object[,]
                {
                    { 1, 1, "2", "Barber 1" },
                    { 2, 2, "3", "Barber 2" }
                });

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
                name: "IX_Barber_IdChiNhanhWork",
                table: "Barber",
                column: "IdChiNhanhWork");

            migrationBuilder.CreateIndex(
                name: "IX_Barber_IdUserBarber",
                table: "Barber",
                column: "IdUserBarber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IdBarber",
                table: "Booking",
                column: "IdBarber");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IdChiNhanh",
                table: "Booking",
                column: "IdChiNhanh");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IdCustomer",
                table: "Booking",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_IdDichVu",
                table: "Booking",
                column: "IdDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_ChiNhanh_IdViTri",
                table: "ChiNhanh",
                column: "IdViTri");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_IdUserCustomer",
                table: "Customer",
                column: "IdUserCustomer",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DichVu_IdDanhMuc",
                table: "DichVu",
                column: "IdDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_HuyLichHen_IdBooking",
                table: "HuyLichHen",
                column: "IdBooking",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichHen_IdBooking",
                table: "LichHen",
                column: "IdBooking",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "HuyLichHen");

            migrationBuilder.DropTable(
                name: "LichHen");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Barber");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "ChiNhanh");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "ViTri");
        }
    }
}
