using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyBanHangAPI.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CtyXuatHoaDon");

            migrationBuilder.DropTable(
                name: "KhachHangOder");

            migrationBuilder.DropTable(
                name: "TaiKhoanNhanThanhToan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CtyXuatHoaDon",
                columns: table => new
                {
                    MaCty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenCty = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CtyXuatHoaDon", x => x.MaCty);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangOder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGoi = table.Column<int>(type: "int", nullable: true),
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: true),
                    MaSoThue = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    TenCongTy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenNguoiLienHe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangOder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachHangOder_GoiDichVu_MaGoi",
                        column: x => x.MaGoi,
                        principalTable: "GoiDichVu",
                        principalColumn: "MaGoi",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KhachHangOder_NhaCungCap_MaNhaCungCap",
                        column: x => x.MaNhaCungCap,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNhaCungCap",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoanNhanThanhToan",
                columns: table => new
                {
                    IdTK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChiNhanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NganHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STKNhan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TenTKNhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoanNhanThanhToan", x => x.IdTK);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CtyXuatHoaDon_TenCty",
                table: "CtyXuatHoaDon",
                column: "TenCty",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangOder_MaGoi",
                table: "KhachHangOder",
                column: "MaGoi");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangOder_MaNhaCungCap",
                table: "KhachHangOder",
                column: "MaNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoanNhanThanhToan_STKNhan",
                table: "TaiKhoanNhanThanhToan",
                column: "STKNhan",
                unique: true);
        }
    }
}
