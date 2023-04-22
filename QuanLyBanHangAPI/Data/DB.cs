using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHangAPI.Data
{
    public class DB : IdentityDbContext<ApplicationUser>
    {
        public DB(DbContextOptions<DB> options) : base(options) { }
        #region DbSet
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<CtyXuatHoaDon> CtyXuatHoaDons { get; set; }
        public DbSet<DonViChuyenPhat> DonViChuyenPhats { get; set; }
        public DbSet<TaiKhoanNhanThanhToan> TaiKhoanNhanThanhToans { get; set; }
        public DbSet<GoiDichVu> GoiDichVus { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<KhachHangOder> KhachHangOders { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<GoiDichVu>(o =>
            //{
            //    o.ToTable("GoiDichVu");
            //    o.HasKey(p => p.MaGoi);
            //    o.Property(p => p.MaNhaCungCap).HasColumnName("MaNhaCungCap");
            //});
        }
    }
}
