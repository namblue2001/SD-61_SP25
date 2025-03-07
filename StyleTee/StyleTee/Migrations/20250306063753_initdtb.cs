using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleTee.Migrations
{
    /// <inheritdoc />
    public partial class initdtb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_DanhMuc_ID_DanhMuc",
                table: "SanPhamChiTiet");

            migrationBuilder.RenameColumn(
                name: "ID_DanhMuc",
                table: "SanPhamChiTiet",
                newName: "ID_KieuDang");

            migrationBuilder.RenameIndex(
                name: "IX_SanPhamChiTiet_ID_DanhMuc",
                table: "SanPhamChiTiet",
                newName: "IX_SanPhamChiTiet_ID_KieuDang");

            migrationBuilder.AddColumn<Guid>(
                name: "ID_DanhMuc",
                table: "SanPham",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "KieuDang",
                columns: table => new
                {
                    ID_KieuDang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenKieuDang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KieuDang", x => x.ID_KieuDang);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_ID_DanhMuc",
                table: "SanPham",
                column: "ID_DanhMuc");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPham_DanhMuc_ID_DanhMuc",
                table: "SanPham",
                column: "ID_DanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "ID_DanhMuc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_KieuDang_ID_KieuDang",
                table: "SanPhamChiTiet",
                column: "ID_KieuDang",
                principalTable: "KieuDang",
                principalColumn: "ID_KieuDang",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SanPham_DanhMuc_ID_DanhMuc",
                table: "SanPham");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_KieuDang_ID_KieuDang",
                table: "SanPhamChiTiet");

            migrationBuilder.DropTable(
                name: "KieuDang");

            migrationBuilder.DropIndex(
                name: "IX_SanPham_ID_DanhMuc",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "ID_DanhMuc",
                table: "SanPham");

            migrationBuilder.RenameColumn(
                name: "ID_KieuDang",
                table: "SanPhamChiTiet",
                newName: "ID_DanhMuc");

            migrationBuilder.RenameIndex(
                name: "IX_SanPhamChiTiet_ID_KieuDang",
                table: "SanPhamChiTiet",
                newName: "IX_SanPhamChiTiet_ID_DanhMuc");

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_DanhMuc_ID_DanhMuc",
                table: "SanPhamChiTiet",
                column: "ID_DanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "ID_DanhMuc",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
