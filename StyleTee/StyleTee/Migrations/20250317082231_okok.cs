using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleTee.Migrations
{
    /// <inheritdoc />
    public partial class okok : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HinhAnh_SanPham_ID_SanPham",
                table: "HinhAnh");

            migrationBuilder.DropIndex(
                name: "IX_HinhAnh_ID_SanPham",
                table: "HinhAnh");

            migrationBuilder.DropColumn(
                name: "ID_SanPham",
                table: "HinhAnh");

            migrationBuilder.RenameColumn(
                name: "Gia",
                table: "SanPhamChiTiet",
                newName: "giaBan");

            migrationBuilder.AddColumn<string>(
                name: "anhDaiDien",
                table: "ThuongHieu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "anhDaiDien",
                table: "SanPhamChiTiet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ngayTao",
                table: "SanPhamChiTiet",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "soLuongTon",
                table: "SanPhamChiTiet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "trangThai",
                table: "SanPham",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "anhDaiDien",
                table: "SanPham",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ngayTao",
                table: "SanPham",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    ID_GioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_TaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.ID_GioHang);
                    table.ForeignKey(
                        name: "FK_GioHang_TaiKhoan_ID_TaiKhoan",
                        column: x => x.ID_TaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "ID_TaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    ID_KhuyenMai = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TyLeKhuyenMai = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMai", x => x.ID_KhuyenMai);
                });

            migrationBuilder.CreateTable(
                name: "PhieuGiamGia",
                columns: table => new
                {
                    Ma_PhieuGiamGia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenPhieuGiamGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LoaiGiamGia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaTriKhuyenMai = table.Column<double>(type: "float", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuongTong = table.Column<int>(type: "int", nullable: false),
                    SoLuongToiDaCho1Nguoi = table.Column<int>(type: "int", nullable: false),
                    GiaTriDonHangToiThieu = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuGiamGia", x => x.Ma_PhieuGiamGia);
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiet",
                columns: table => new
                {
                    ID_GioHangChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_GioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_SanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    donGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiet", x => x.ID_GioHangChiTiet);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_GioHang_ID_GioHang",
                        column: x => x.ID_GioHang,
                        principalTable: "GioHang",
                        principalColumn: "ID_GioHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_SanPhamChiTiet_ID_SanPhamChiTiet",
                        column: x => x.ID_SanPhamChiTiet,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID_SanPhamChiTiet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_ID_TaiKhoan",
                table: "GioHang",
                column: "ID_TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_ID_GioHang",
                table: "GioHangChiTiet",
                column: "ID_GioHang");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_ID_SanPhamChiTiet",
                table: "GioHangChiTiet",
                column: "ID_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "UQ_TyLeKhuyenMai",
                table: "KhuyenMai",
                columns: new[] { "TyLeKhuyenMai", "NgayBatDau" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TenPhieuGiamGia",
                table: "PhieuGiamGia",
                column: "TenPhieuGiamGia",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GioHangChiTiet");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "PhieuGiamGia");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropColumn(
                name: "anhDaiDien",
                table: "ThuongHieu");

            migrationBuilder.DropColumn(
                name: "anhDaiDien",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "ngayTao",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "soLuongTon",
                table: "SanPhamChiTiet");

            migrationBuilder.DropColumn(
                name: "anhDaiDien",
                table: "SanPham");

            migrationBuilder.DropColumn(
                name: "ngayTao",
                table: "SanPham");

            migrationBuilder.RenameColumn(
                name: "giaBan",
                table: "SanPhamChiTiet",
                newName: "Gia");

            migrationBuilder.AlterColumn<bool>(
                name: "trangThai",
                table: "SanPham",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ID_SanPham",
                table: "HinhAnh",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnh_ID_SanPham",
                table: "HinhAnh",
                column: "ID_SanPham");

            migrationBuilder.AddForeignKey(
                name: "FK_HinhAnh_SanPham_ID_SanPham",
                table: "HinhAnh",
                column: "ID_SanPham",
                principalTable: "SanPham",
                principalColumn: "ID_SanPham",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
