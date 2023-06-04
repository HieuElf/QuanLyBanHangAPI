using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyBanHangAPI.Migrations
{
    public partial class updatedonhang1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhuongThucThanhToan",
                table: "DonDatHang",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhuongThucThanhToan",
                table: "DonDatHang");
        }
    }
}
