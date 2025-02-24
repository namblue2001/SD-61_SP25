using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleTee.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaiKhoan_VaiTro_ID_VaiTro",
                table: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "VaiTro");

            migrationBuilder.DropIndex(
                name: "IX_TaiKhoan_ID_VaiTro",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "ID_VaiTro",
                table: "TaiKhoan");

            migrationBuilder.AlterColumn<string>(
                name: "trangThai",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "soDienThoai",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "hoTen",
                table: "TaiKhoan",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "anhDaiDien",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tenVaiTro",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "anhDaiDien",
                table: "TaiKhoan");

            migrationBuilder.DropColumn(
                name: "tenVaiTro",
                table: "TaiKhoan");

            migrationBuilder.AlterColumn<string>(
                name: "trangThai",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "soDienThoai",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "hoTen",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "TaiKhoan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ID_VaiTro",
                table: "TaiKhoan",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VaiTro",
                columns: table => new
                {
                    ID_VaiTro = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_TaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenVaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTro", x => x.ID_VaiTro);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_ID_VaiTro",
                table: "TaiKhoan",
                column: "ID_VaiTro",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaiKhoan_VaiTro_ID_VaiTro",
                table: "TaiKhoan",
                column: "ID_VaiTro",
                principalTable: "VaiTro",
                principalColumn: "ID_VaiTro",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
