using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleTee.Migrations
{
    /// <inheritdoc />
    public partial class initdbb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiaChi_TaiKhoan_ID_TaiKhoan",
                table: "DiaChi");

            migrationBuilder.DropForeignKey(
                name: "FK_HinhAnh_SanPhamChiTiet_ID_SanPhamChiTiet",
                table: "HinhAnh");

            migrationBuilder.DropForeignKey(
                name: "FK_HinhAnh_SanPham_ID_SanPham",
                table: "HinhAnh");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_ChatLieu_ID_ChatLieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_DanhMuc_ID_DanhMuc",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_KichThuoc_ID_Size",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_MauSac_ID_Mau",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_SanPham_ID_SanPham",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_ThuongHieu_ID_ThuongHieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_XuatXu_ID_XuatXu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_ID_ChatLieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_ID_DanhMuc",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_ID_Mau",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_ID_SanPham",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_ID_Size",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_ID_ThuongHieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_ID_XuatXu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_HinhAnh_ID_SanPham",
                table: "HinhAnh");

            migrationBuilder.DropIndex(
                name: "IX_HinhAnh_ID_SanPhamChiTiet",
                table: "HinhAnh");

            migrationBuilder.DropIndex(
                name: "IX_DiaChi_ID_TaiKhoan",
                table: "DiaChi");

            migrationBuilder.AddColumn<Guid>(
                name: "ChatLieuID_ChatLieu",
                table: "SanPhamChiTiet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DanhMucID_DanhMuc",
                table: "SanPhamChiTiet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "KichThuocID_KichThuoc",
                table: "SanPhamChiTiet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MauSacID_MauSac",
                table: "SanPhamChiTiet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SanPhamID_SanPham",
                table: "SanPhamChiTiet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ThuongHieuID_ThuongHieu",
                table: "SanPhamChiTiet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "XuatXuID_XuatXu",
                table: "SanPhamChiTiet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SanPhamChiTietID_SanPhamChiTiet",
                table: "HinhAnh",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SanPhamID_SanPham",
                table: "HinhAnh",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TaiKhoanID_TaiKhoan",
                table: "DiaChi",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ChatLieuID_ChatLieu",
                table: "SanPhamChiTiet",
                column: "ChatLieuID_ChatLieu");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_DanhMucID_DanhMuc",
                table: "SanPhamChiTiet",
                column: "DanhMucID_DanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_KichThuocID_KichThuoc",
                table: "SanPhamChiTiet",
                column: "KichThuocID_KichThuoc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_MauSacID_MauSac",
                table: "SanPhamChiTiet",
                column: "MauSacID_MauSac");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_SanPhamID_SanPham",
                table: "SanPhamChiTiet",
                column: "SanPhamID_SanPham");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ThuongHieuID_ThuongHieu",
                table: "SanPhamChiTiet",
                column: "ThuongHieuID_ThuongHieu");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_XuatXuID_XuatXu",
                table: "SanPhamChiTiet",
                column: "XuatXuID_XuatXu");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnh_SanPhamChiTietID_SanPhamChiTiet",
                table: "HinhAnh",
                column: "SanPhamChiTietID_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnh_SanPhamID_SanPham",
                table: "HinhAnh",
                column: "SanPhamID_SanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DiaChi_TaiKhoanID_TaiKhoan",
                table: "DiaChi",
                column: "TaiKhoanID_TaiKhoan");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaChi_TaiKhoan_TaiKhoanID_TaiKhoan",
                table: "DiaChi",
                column: "TaiKhoanID_TaiKhoan",
                principalTable: "TaiKhoan",
                principalColumn: "ID_TaiKhoan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HinhAnh_SanPhamChiTiet_SanPhamChiTietID_SanPhamChiTiet",
                table: "HinhAnh",
                column: "SanPhamChiTietID_SanPhamChiTiet",
                principalTable: "SanPhamChiTiet",
                principalColumn: "ID_SanPhamChiTiet",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HinhAnh_SanPham_SanPhamID_SanPham",
                table: "HinhAnh",
                column: "SanPhamID_SanPham",
                principalTable: "SanPham",
                principalColumn: "ID_SanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_ChatLieu_ChatLieuID_ChatLieu",
                table: "SanPhamChiTiet",
                column: "ChatLieuID_ChatLieu",
                principalTable: "ChatLieu",
                principalColumn: "ID_ChatLieu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_DanhMuc_DanhMucID_DanhMuc",
                table: "SanPhamChiTiet",
                column: "DanhMucID_DanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "ID_DanhMuc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_KichThuoc_KichThuocID_KichThuoc",
                table: "SanPhamChiTiet",
                column: "KichThuocID_KichThuoc",
                principalTable: "KichThuoc",
                principalColumn: "ID_KichThuoc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_MauSac_MauSacID_MauSac",
                table: "SanPhamChiTiet",
                column: "MauSacID_MauSac",
                principalTable: "MauSac",
                principalColumn: "ID_MauSac",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_SanPham_SanPhamID_SanPham",
                table: "SanPhamChiTiet",
                column: "SanPhamID_SanPham",
                principalTable: "SanPham",
                principalColumn: "ID_SanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_ThuongHieu_ThuongHieuID_ThuongHieu",
                table: "SanPhamChiTiet",
                column: "ThuongHieuID_ThuongHieu",
                principalTable: "ThuongHieu",
                principalColumn: "ID_ThuongHieu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_XuatXu_XuatXuID_XuatXu",
                table: "SanPhamChiTiet",
                column: "XuatXuID_XuatXu",
                principalTable: "XuatXu",
                principalColumn: "ID_XuatXu",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiaChi_TaiKhoan_TaiKhoanID_TaiKhoan",
                table: "DiaChi");

            migrationBuilder.DropForeignKey(
                name: "FK_HinhAnh_SanPhamChiTiet_SanPhamChiTietID_SanPhamChiTiet",
                table: "HinhAnh");

            migrationBuilder.DropForeignKey(
                name: "FK_HinhAnh_SanPham_SanPhamID_SanPham",
                table: "HinhAnh");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_ChatLieu_ChatLieuID_ChatLieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_DanhMuc_DanhMucID_DanhMuc",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_KichThuoc_KichThuocID_KichThuoc",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_MauSac_MauSacID_MauSac",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_SanPham_SanPhamID_SanPham",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_ThuongHieu_ThuongHieuID_ThuongHieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPhamChiTiet_XuatXu_XuatXuID_XuatXu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_ChatLieuID_ChatLieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_DanhMucID_DanhMuc",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_KichThuocID_KichThuoc",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_MauSacID_MauSac",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_SanPhamID_SanPham",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_ThuongHieuID_ThuongHieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_SanPhamChiTiet_XuatXuID_XuatXu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropIndex(
                name: "IX_HinhAnh_SanPhamChiTietID_SanPhamChiTiet",
                table: "HinhAnh");

            migrationBuilder.DropIndex(
                name: "IX_HinhAnh_SanPhamID_SanPham",
                table: "HinhAnh");

            migrationBuilder.DropIndex(
                name: "IX_DiaChi_TaiKhoanID_TaiKhoan",
                table: "DiaChi");

            migrationBuilder.DropColumn(
                name: "ChatLieuID_ChatLieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "DanhMucID_DanhMuc",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "KichThuocID_KichThuoc",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "MauSacID_MauSac",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "SanPhamID_SanPham",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "ThuongHieuID_ThuongHieu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "XuatXuID_XuatXu",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "SanPhamChiTietID_SanPhamChiTiet",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "SanPhamID_SanPham",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "TaiKhoanID_TaiKhoan",
                table: "DiaChi");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ID_ChatLieu",
                table: "SanPhamChiTiet",
                column: "ID_ChatLieu");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ID_DanhMuc",
                table: "SanPhamChiTiet",
                column: "ID_DanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ID_Mau",
                table: "SanPhamChiTiet",
                column: "ID_Mau");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ID_SanPham",
                table: "SanPhamChiTiet",
                column: "ID_SanPham");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ID_Size",
                table: "SanPhamChiTiet",
                column: "ID_Size");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ID_ThuongHieu",
                table: "SanPhamChiTiet",
                column: "ID_ThuongHieu");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ID_XuatXu",
                table: "SanPhamChiTiet",
                column: "ID_XuatXu");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnh_ID_SanPham",
                table: "HinhAnh",
                column: "ID_SanPham");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnh_ID_SanPhamChiTiet",
                table: "HinhAnh",
                column: "ID_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_DiaChi_ID_TaiKhoan",
                table: "DiaChi",
                column: "ID_TaiKhoan");

            migrationBuilder.AddForeignKey(
                name: "FK_DiaChi_TaiKhoan_ID_TaiKhoan",
                table: "DiaChi",
                column: "ID_TaiKhoan",
                principalTable: "TaiKhoan",
                principalColumn: "ID_TaiKhoan",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HinhAnh_SanPhamChiTiet_ID_SanPhamChiTiet",
                table: "HinhAnh",
                column: "ID_SanPhamChiTiet",
                principalTable: "SanPhamChiTiet",
                principalColumn: "ID_SanPhamChiTiet",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HinhAnh_SanPham_ID_SanPham",
                table: "HinhAnh",
                column: "ID_SanPham",
                principalTable: "SanPham",
                principalColumn: "ID_SanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_ChatLieu_ID_ChatLieu",
                table: "SanPhamChiTiet",
                column: "ID_ChatLieu",
                principalTable: "ChatLieu",
                principalColumn: "ID_ChatLieu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_DanhMuc_ID_DanhMuc",
                table: "SanPhamChiTiet",
                column: "ID_DanhMuc",
                principalTable: "DanhMuc",
                principalColumn: "ID_DanhMuc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_KichThuoc_ID_Size",
                table: "SanPhamChiTiet",
                column: "ID_Size",
                principalTable: "KichThuoc",
                principalColumn: "ID_KichThuoc",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_MauSac_ID_Mau",
                table: "SanPhamChiTiet",
                column: "ID_Mau",
                principalTable: "MauSac",
                principalColumn: "ID_MauSac",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_SanPham_ID_SanPham",
                table: "SanPhamChiTiet",
                column: "ID_SanPham",
                principalTable: "SanPham",
                principalColumn: "ID_SanPham",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_ThuongHieu_ID_ThuongHieu",
                table: "SanPhamChiTiet",
                column: "ID_ThuongHieu",
                principalTable: "ThuongHieu",
                principalColumn: "ID_ThuongHieu",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPhamChiTiet_XuatXu_ID_XuatXu",
                table: "SanPhamChiTiet",
                column: "ID_XuatXu",
                principalTable: "XuatXu",
                principalColumn: "ID_XuatXu",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
