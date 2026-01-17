using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiayTheThao.Migrations
{
    /// <inheritdoc />
    public partial class ThemTrangThaiChoLienHe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "LienHe",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "LienHe");
        }
    }
}
