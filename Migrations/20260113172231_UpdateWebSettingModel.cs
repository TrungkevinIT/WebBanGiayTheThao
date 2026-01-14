using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiayTheThao.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWebSettingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "WebSetting",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Linkfacebook",
                table: "WebSetting",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mota",
                table: "WebSetting",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "WebSetting");

            migrationBuilder.DropColumn(
                name: "Linkfacebook",
                table: "WebSetting");

            migrationBuilder.DropColumn(
                name: "Mota",
                table: "WebSetting");
        }
    }
}
