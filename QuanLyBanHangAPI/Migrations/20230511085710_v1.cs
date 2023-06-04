using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyBanHangAPI.Migrations
{
    public partial class v1 : Migration
    {
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
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "ChuyenMucBlog",
                columns: table => new
                {
                    maChuyenMuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenChuyenMuc = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuyenMucBlog", x => x.maChuyenMuc);
                });

            migrationBuilder.CreateTable(
                name: "CtyXuatHoaDon",
                columns: table => new
                {
                    MaCty = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCty = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CtyXuatHoaDon", x => x.MaCty);
                });

            migrationBuilder.CreateTable(
                name: "DonViChuyenPhat",
                columns: table => new
                {
                    MaDonViChuyenPhat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDonVi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonViChuyenPhat", x => x.MaDonViChuyenPhat);
                });

            migrationBuilder.CreateTable(
                name: "GoiDichVu",
                columns: table => new
                {
                    MaGoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenGoi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoiDichVu", x => x.MaGoi);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrangChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.MaNhaCungCap);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoanNhanThanhToan",
                columns: table => new
                {
                    IdTK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTKNhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    STKNhan = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NganHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChiNhanh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoanNhanThanhToan", x => x.IdTK);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VaiLidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenIsUsed = table.Column<bool>(type: "bit", nullable: false),
                    TokenIsReVoked = table.Column<bool>(type: "bit", nullable: false),
                    ReFreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
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
                name: "Blog",
                columns: table => new
                {
                    maBaiViet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maChuyenMuc = table.Column<int>(type: "int", nullable: false),
                    tenBaiViet = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tomTat = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    anhBia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    noiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    anhBaiViet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngayBaiViet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayChinhSuaCuoi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    luotXem = table.Column<int>(type: "int", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.maBaiViet);
                    table.ForeignKey(
                        name: "FK_Blog_ChuyenMucBlog_maChuyenMuc",
                        column: x => x.maChuyenMuc,
                        principalTable: "ChuyenMucBlog",
                        principalColumn: "maChuyenMuc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonDatHang",
                columns: table => new
                {
                    MaDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDatHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaDonViChuyenPhat = table.Column<int>(type: "int", nullable: false),
                    TinhTrangGiaoHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayGiao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThaiDonHang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonDatHang", x => x.MaDonHang);
                    table.ForeignKey(
                        name: "FK_DonDatHang_DonViChuyenPhat_MaDonViChuyenPhat",
                        column: x => x.MaDonViChuyenPhat,
                        principalTable: "DonViChuyenPhat",
                        principalColumn: "MaDonViChuyenPhat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangOder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSoThue = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TenCongTy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenNguoiLienHe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: true),
                    MaGoi = table.Column<int>(type: "int", nullable: true)
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
                name: "SanPham",
                columns: table => new
                {
                    MaSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaGoi = table.Column<int>(type: "int", nullable: true),
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: true),
                    TomTat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuotXem = table.Column<int>(type: "int", nullable: false),
                    DaBan = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    GiamGia = table.Column<double>(type: "float", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayChinhSuaCuoi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK_SanPham_GoiDichVu_MaGoi",
                        column: x => x.MaGoi,
                        principalTable: "GoiDichVu",
                        principalColumn: "MaGoi",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SanPham_NhaCungCap_MaNhaCungCap",
                        column: x => x.MaNhaCungCap,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNhaCungCap",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    MaChiTietDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenSP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonGia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => x.MaChiTietDonHang);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonDatHang_MaDonHang",
                        column: x => x.MaDonHang,
                        principalTable: "DonDatHang",
                        principalColumn: "MaDonHang",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Blog_maChuyenMuc",
                table: "Blog",
                column: "maChuyenMuc");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_tenBaiViet",
                table: "Blog",
                column: "tenBaiViet",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_MaDonHang",
                table: "ChiTietDonHang",
                column: "MaDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChuyenMucBlog_tenChuyenMuc",
                table: "ChuyenMucBlog",
                column: "tenChuyenMuc",
                unique: true,
                filter: "[tenChuyenMuc] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CtyXuatHoaDon_TenCty",
                table: "CtyXuatHoaDon",
                column: "TenCty",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonDatHang_MaDonViChuyenPhat",
                table: "DonDatHang",
                column: "MaDonViChuyenPhat");

            migrationBuilder.CreateIndex(
                name: "IX_DonViChuyenPhat_TenDonVi",
                table: "DonViChuyenPhat",
                column: "TenDonVi",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoiDichVu_TenGoi",
                table: "GoiDichVu",
                column: "TenGoi",
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
                name: "IX_NhaCungCap_TenNhaCungCap",
                table: "NhaCungCap",
                column: "TenNhaCungCap",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaGoi",
                table: "SanPham",
                column: "MaGoi");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaNhaCungCap",
                table: "SanPham",
                column: "MaNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_TenSP",
                table: "SanPham",
                column: "TenSP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoanNhanThanhToan_STKNhan",
                table: "TaiKhoanNhanThanhToan",
                column: "STKNhan",
                unique: true);
        }

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
                name: "Blog");

            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "CtyXuatHoaDon");

            migrationBuilder.DropTable(
                name: "KhachHangOder");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "TaiKhoanNhanThanhToan");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ChuyenMucBlog");

            migrationBuilder.DropTable(
                name: "DonDatHang");

            migrationBuilder.DropTable(
                name: "GoiDichVu");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "DonViChuyenPhat");
        }
    }
}
