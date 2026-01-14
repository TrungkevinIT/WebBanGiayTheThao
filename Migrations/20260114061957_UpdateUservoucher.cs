using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiayTheThao.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUservoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GiaTriGiamLuu",
                table: "UserVoucher",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaCodeLuu",
                table: "UserVoucher",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDauLuu",
                table: "UserVoucher",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetThucLuu",
                table: "UserVoucher",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "BinhLuan",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaTriGiamLuu",
                table: "UserVoucher");

            migrationBuilder.DropColumn(
                name: "MaCodeLuu",
                table: "UserVoucher");

            migrationBuilder.DropColumn(
                name: "NgayBatDauLuu",
                table: "UserVoucher");

            migrationBuilder.DropColumn(
                name: "NgayKetThucLuu",
                table: "UserVoucher");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "BinhLuan");
        }
    }
}
