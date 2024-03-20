using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Storage.Configuration;
using Storage.Models;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading;

namespace Storage
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ViTri> ViTri { get; set; }
        public DbSet<ChiNhanh> ChiNhanh { get; set; }
        public DbSet<DanhMuc> DanhMuc { get; set; }
        public DbSet<DichVu> DichVu { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Barber> Barber { get; set; }
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
        public DbSet<LichHen> LichHen { get; set; }
        public DbSet<HuyLichHen> HuyLichHen { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
            SeedAdmin(builder);
            SeedUserBarbers(builder);
            SeedBarbers(builder);
            builder.ApplyConfiguration(new ViTriConfiguration());
            SeedViTris(builder);
            builder.ApplyConfiguration(new ChiNhanhConfiguration());
            SeedChiNhanhs(builder);

            builder.ApplyConfiguration(new DanhMucConfiguration());
            SeedDanhMucs(builder);
            builder.ApplyConfiguration(new DichVuConfiguration());
            SeedDichVus(builder);

            builder.ApplyConfiguration(new BookingConfiguration());
            builder.ApplyConfiguration(new LichHenConfiguration());
            builder.ApplyConfiguration(new HuyLichHenConfiguration());
            builder.ApplyConfiguration(new AspNetUsersConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new BarberConfiguration());

        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
            (
                new IdentityRole() { Id = "1", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "2", Name = "Barber", ConcurrencyStamp = "1", NormalizedName = "Barber" },
                new IdentityRole() { Id = "3", Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" }
            );
        }
        private static void SeedAdmin(ModelBuilder builder)
        {
            // Thêm users và gán roles cho users
            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>().HasData
            (
                new IdentityUser
                {
                    Id = "1",
                    UserName = "Admin",
                    NormalizedUserName = "Admin",
                    Email = "Admin@admin.com",
                    NormalizedEmail = "Admin@admin.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Aa@123"),
                    SecurityStamp = string.Empty
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData
            (
                new IdentityUserRole<string> { RoleId = "1", UserId = "1" }
            );
        }
        private static void SeedUserBarbers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var barbers = new List<IdentityUser>
            {
                new IdentityUser
                {
                    Id = "2",
                    UserName = "Barber1",
                    NormalizedUserName = "Barber1",
                    Email = "Barber1@barber.com",
                    NormalizedEmail = "Barber1@barber.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Aa@123"),
                    SecurityStamp = string.Empty
                },
                new IdentityUser
                {
                    Id = "3",
                    UserName = "Barber2",
                    NormalizedUserName = "Barber2",
                    Email = "Barber2@barber.com",
                    NormalizedEmail = "Barber2@barber.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Aa@123"),
                    SecurityStamp = string.Empty
                },
                // Thêm các barber khác nếu cần
            };

            foreach (var barber in barbers)
            {
                builder.Entity<IdentityUser>().HasData(barber);
                builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = "2", UserId = barber.Id });
            }
        }


        private static void SeedViTris(ModelBuilder builder)
        {
            builder.Entity<ViTri>().HasData
                (
                    new ViTri() { Id = 1, TinhThanhPho = "Hồ Chí Minh"},
                    new ViTri() { Id = 2, TinhThanhPho = "Hà Nội"},
                    new ViTri() { Id = 3, TinhThanhPho = "Hải Phòng"},
                    new ViTri() { Id = 4, TinhThanhPho = "Bình Phước"}
                );
        }
        private static void SeedChiNhanhs(ModelBuilder builder)
        {
            builder.Entity<ChiNhanh>().HasData
                (
                    new ChiNhanh() { Id = 1, TenChiNhanh = "Barber Đỗ Xuân Hợp", DiaChi = "502 Đỗ Xuân Hợp, Phước Long B, TP. Thủ Đức", IdViTri = 1 },
                    new ChiNhanh() { Id = 2, TenChiNhanh = "Barber Lê Văn Việt", DiaChi = "302 Lê Văn Việt, Phường Hiệp Phú, TP. Thủ Đức", IdViTri = 1 },
                    new ChiNhanh() { Id = 3, TenChiNhanh = "Barber Lê Thị Riêng", DiaChi = "113 Lê Thị Riêng, Bến Thành, Quận 1, TP.Hồ Chí Minh", IdViTri = 1 },
                    new ChiNhanh() { Id = 4, TenChiNhanh = "Barber Lê Văn Sỹ", DiaChi = "339 Lê Văn Sỹ, Phường 13, Quận 3, TP. Hồ Chí Minh", IdViTri = 1 },

                    new ChiNhanh() { Id = 5, TenChiNhanh = "Barber Tuệ Tĩnh", DiaChi = "Số 5 Tuệ Tĩnh, Hai Bà Trưng, Hà Nội", IdViTri = 2 },
                    new ChiNhanh() { Id = 6, TenChiNhanh = "Barber Triệu Việt Vương", DiaChi = "Số 38A Triệu Việt Vương, Hai Bà Trưng, Hà Nội", IdViTri = 2 },
                    new ChiNhanh() { Id = 7, TenChiNhanh = "Barber Phạm Hồng Thái", DiaChi = "Số 37 Phạm Hồng Thái, Trúc Bạch, Ba Đình, Hà Nội", IdViTri = 2 },
                    new ChiNhanh() { Id = 8, TenChiNhanh = "Barber Hạ Hồi", DiaChi = "Số 30 Hạ Hồi, Hoàn Kiếm, Hà Nội", IdViTri = 2 },

                    new ChiNhanh() { Id = 9, TenChiNhanh = "Barber Đông Khê", DiaChi = "Số 20 Đường Đông Khê 1, Ngô Quyền, Hải Phòng", IdViTri = 3 },
                    new ChiNhanh() { Id = 10, TenChiNhanh = "Barber Hải Triều", DiaChi = "Số 86 Hải Triều, Quán Toan, Hải Phòng", IdViTri = 3 },
                    new ChiNhanh() { Id = 11, TenChiNhanh = "Barber Lê Đại Hành", DiaChi = "24 Lê Đại Hành, Hồng Bàng, Hải Phòng", IdViTri = 3 },
                    new ChiNhanh() { Id = 12, TenChiNhanh = "Barber Lạch Tray", DiaChi = "100 Lạch Tray, Ngô Quyền, Hải Phòng", IdViTri = 3 },

                    new ChiNhanh() { Id = 13, TenChiNhanh = "Barber Phú Riềng Đỏ", DiaChi = "722 Phú Riềng Đỏ, P Tân Xuân, Tp Đồng Xoài, Bình Phước", IdViTri = 4 },
                    new ChiNhanh() { Id = 14, TenChiNhanh = "Barber Nguyễn Huệ", DiaChi = "Đường Nguyễn Huệ, Kp3, P. Tân Đồng, TX. Đồng Xoài, Bình Phước", IdViTri = 4 },
                    new ChiNhanh() { Id = 15, TenChiNhanh = "Barber Hùng Vương", DiaChi = "87 Hùng Vương, Tân Bình, Đồng Xoài, Bình Phước", IdViTri = 4 }
                );
        }
        private static void SeedBarbers(ModelBuilder builder)
        {
            var barbers = new List<Barber>
            {
                new Barber
                {
                    Id = 1,
                    NameBarber = "Barber 1",
                    IdChiNhanhWork = 1, // ID của chi nhánh mà Barber làm việc
                    IdUserBarber = "2" // ID của user tương ứng với Barber
                },
                new Barber
                {
                    Id = 2,
                    NameBarber = "Barber 2",
                    IdChiNhanhWork = 2,
                    IdUserBarber = "3"
                },
                // Thêm các barber khác nếu cần
            };

            foreach (var barber in barbers)
            {
                builder.Entity<Barber>().HasData(barber);
            }
        }
        private static void SeedDanhMucs(ModelBuilder builder)
        {
            builder.Entity<DanhMuc>().HasData
                (
                    new DanhMuc() { Id = 1, TenDanhMuc = "Cắt Tóc", MoTaDanhMuc = "Bao gồm các dịch vụ cắt gội massage thư giản" },
                    new DanhMuc() { Id = 2, TenDanhMuc = "Uốn Tóc", MoTaDanhMuc = "Các dịch vụ uốn tóc cao cấp" },
                    new DanhMuc() { Id = 3, TenDanhMuc = "Duỗi Tóc", MoTaDanhMuc = "Duỗi thẳng tóc xoăn tóc gãy" },
                    new DanhMuc() { Id = 4, TenDanhMuc = "Nhuộm Tóc", MoTaDanhMuc = "Các dịch vụ nhuộm tóc cao cấp" },
                    new DanhMuc() { Id = 5, TenDanhMuc = "Phục Hồi Tóc", MoTaDanhMuc = "Phục hồi tóc bị hư tổn" }
                );
        }
        private static void SeedDichVus(ModelBuilder builder)
        {
            builder.Entity<DichVu>().HasData
                (
                     new DichVu() { Id = 1, TenDichVu = "Cắt tóc mái", MoTa = "Tạo kiểu mái phù hợp với khuôn mặt và phong cách cá nhân", Gia = 60000, IdDanhMuc = 1 },
                     new DichVu() { Id = 2, TenDichVu = "Cắt tóc đều", MoTa = "Cắt tóc để tạo ra độ dài và kiểu dáng mong muốn trên toàn bộ tóc", Gia = 120000, IdDanhMuc = 1 },
                     new DichVu() { Id = 3, TenDichVu = "Cắt tóc tạo kiểu", MoTa = "Sấy và làm tóc xoăn hoặc làm thẳng", Gia = 150000, IdDanhMuc = 1 },

                     new DichVu() { Id = 4, TenDichVu = "Uốn tóc nhanh", MoTa = "Sấy tóc để tạo kiểu xoăn hoặc thẳng nhanh chóng", Gia = 320000, IdDanhMuc = 2 },
                     new DichVu() { Id = 5, TenDichVu = "Uốn tóc lâu dài", MoTa = "Sử dụng hóa chất để tạo kiểu xoăn tóc kéo dài thời gian", Gia = 700000, IdDanhMuc = 2 },
                     new DichVu() { Id = 6, TenDichVu = "Uốn tóc xù", MoTa = "Tạo kiểu tóc với những sóng tự nhiên giống như kiểu tóc sau khi đi biển", Gia = 720000, IdDanhMuc = 2 },

                     new DichVu() { Id = 7, TenDichVu = "Duỗi tóc keratin", MoTa = "Sử dụng sản phẩm chứa keratin để giảm độ xoăn và tăng độ mềm mại cho tóc", Gia = 500000, IdDanhMuc = 3 },
                     new DichVu() { Id = 8, TenDichVu = "Duỗi tóc máy laze", MoTa = "Sử dụng máy laze để làm thẳng tóc", Gia = 500000, IdDanhMuc = 3 },
                     new DichVu() { Id = 9, TenDichVu = "Duỗi tóc ion hơi nước", MoTa = "Sử dụng hơi nước và thiết bị ion để làm thẳng tóc", Gia = 500000, IdDanhMuc = 3 },
                     new DichVu() { Id = 10, TenDichVu = "Duỗi tóc ổn định", MoTa = "Sử dụng hóa chất để làm thẳng tóc và giữ độ thẳng lâu dài", Gia = 500000, IdDanhMuc = 3 },

                     new DichVu() { Id = 11, TenDichVu = "Nhuộm toàn bộ tóc", MoTa = "Thay đổi màu sắc của toàn bộ bộ tóc", Gia = 900000, IdDanhMuc = 4 },
                     new DichVu() { Id = 12, TenDichVu = "Nhuộm phần tóc", MoTa = "Nhuộm chỉ một phần cụ thể của tóc, như mái, đuôi, hoặc mèo", Gia = 500000, IdDanhMuc = 4 },
                     new DichVu() { Id = 13, TenDichVu = "Highlight", MoTa = "Tạo những sợi tóc nhấn màu, thường là những sợi tóc màu sáng hơn so với màu tự nhiên của tóc", Gia = 950000, IdDanhMuc = 4 },

                     new DichVu() { Id = 14, TenDichVu = " Phục hồi bằng dầu nhiệt đới", MoTa = "Sử dụng dầu nhiệt đới để làm nóng tóc và tăng cường dưỡng chất cho tóc", Gia = 800000, IdDanhMuc = 5 },
                     new DichVu() { Id = 15, TenDichVu = "Phục hồi màu tóc", MoTa = "Cho những người đã nhuộm tóc, dịch vụ này giúp bảo quản và tái tạo màu sắc của tóc", Gia = 820000, IdDanhMuc = 5 }
                );
        }

    }
}
