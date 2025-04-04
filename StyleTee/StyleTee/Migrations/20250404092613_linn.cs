using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StyleTee.Migrations
{
    /// <inheritdoc />
    public partial class linn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "diaChiVanChuyen",
                table: "DonHang");

            migrationBuilder.RenameColumn(
                name: "phuongThucThanhToan",
                table: "DonHang",
                newName: "ghiChu");

            migrationBuilder.AddColumn<Guid>(
                name: "ID_MaGiamGia",
                table: "DonHang",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ID_PhuongThucThanhToan",
                table: "DonHang",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ID_ThongTinVanChuyen",
                table: "DonHang",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "phiVanChuyen",
                table: "DonHang",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    ID_HoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_DonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ngayVanChuyen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayNhanHang = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.ID_HoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDon_DonHang_ID_DonHang",
                        column: x => x.ID_DonHang,
                        principalTable: "DonHang",
                        principalColumn: "ID_DonHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhuongThucThanhToan",
                columns: table => new
                {
                    ID_PTTT = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenPhuongThuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuongThucThanhToan", x => x.ID_PTTT);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinVanChuyen",
                columns: table => new
                {
                    ID_VanChuyen = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    xa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    huyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tinh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinVanChuyen", x => x.ID_VanChuyen);
                });

            migrationBuilder.CreateTable(
                name: "YeuCauDoiTra",
                columns: table => new
                {
                    ID_YeuCauDoiTra = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_DonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    loaiYeuCau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    liDoDoiTra = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YeuCauDoiTra", x => x.ID_YeuCauDoiTra);
                    table.ForeignKey(
                        name: "FK_YeuCauDoiTra_DonHang_ID_DonHang",
                        column: x => x.ID_DonHang,
                        principalTable: "DonHang",
                        principalColumn: "ID_DonHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnhMinhChung",
                columns: table => new
                {
                    ID_AnhMinhChung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_YeuCauDoiTra = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnhMinhChung", x => x.ID_AnhMinhChung);
                    table.ForeignKey(
                        name: "FK_AnhMinhChung_YeuCauDoiTra_ID_YeuCauDoiTra",
                        column: x => x.ID_YeuCauDoiTra,
                        principalTable: "YeuCauDoiTra",
                        principalColumn: "ID_YeuCauDoiTra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamDoiTra",
                columns: table => new
                {
                    ID_SPDoiTra = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_ChiTietDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_YeuCauDoiTra = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamDoiTra", x => x.ID_SPDoiTra);
                    table.ForeignKey(
                        name: "FK_SanPhamDoiTra_ChiTietDonHang_ID_ChiTietDonHang",
                        column: x => x.ID_ChiTietDonHang,
                        principalTable: "ChiTietDonHang",
                        principalColumn: "ID_DonHangChiTiet"
                       );
                    table.ForeignKey(
                        name: "FK_SanPhamDoiTra_YeuCauDoiTra_ID_YeuCauDoiTra",
                        column: x => x.ID_YeuCauDoiTra,
                        principalTable: "YeuCauDoiTra",
                        principalColumn: "ID_YeuCauDoiTra"
                       );
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_ID_MaGiamGia",
                table: "DonHang",
                column: "ID_MaGiamGia");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_ID_PhuongThucThanhToan",
                table: "DonHang",
                column: "ID_PhuongThucThanhToan");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_ID_ThongTinVanChuyen",
                table: "DonHang",
                column: "ID_ThongTinVanChuyen");

            migrationBuilder.CreateIndex(
                name: "IX_AnhMinhChung_ID_YeuCauDoiTra",
                table: "AnhMinhChung",
                column: "ID_YeuCauDoiTra");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_ID_DonHang",
                table: "HoaDon",
                column: "ID_DonHang");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamDoiTra_ID_ChiTietDonHang",
                table: "SanPhamDoiTra",
                column: "ID_ChiTietDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamDoiTra_ID_YeuCauDoiTra",
                table: "SanPhamDoiTra",
                column: "ID_YeuCauDoiTra");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauDoiTra_ID_DonHang",
                table: "YeuCauDoiTra",
                column: "ID_DonHang");

            migrationBuilder.AddForeignKey(
                name: "FK_DonHang_PhieuGiamGia_ID_MaGiamGia",
                table: "DonHang",
                column: "ID_MaGiamGia",
                principalTable: "PhieuGiamGia",
                principalColumn: "Ma_PhieuGiamGia",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonHang_PhuongThucThanhToan_ID_PhuongThucThanhToan",
                table: "DonHang",
                column: "ID_PhuongThucThanhToan",
                principalTable: "PhuongThucThanhToan",
                principalColumn: "ID_PTTT",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DonHang_ThongTinVanChuyen_ID_ThongTinVanChuyen",
                table: "DonHang",
                column: "ID_ThongTinVanChuyen",
                principalTable: "ThongTinVanChuyen",
                principalColumn: "ID_VanChuyen",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonHang_PhieuGiamGia_ID_MaGiamGia",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK_DonHang_PhuongThucThanhToan_ID_PhuongThucThanhToan",
                table: "DonHang");

            migrationBuilder.DropForeignKey(
                name: "FK_DonHang_ThongTinVanChuyen_ID_ThongTinVanChuyen",
                table: "DonHang");

            migrationBuilder.DropTable(
                name: "AnhMinhChung");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "PhuongThucThanhToan");

            migrationBuilder.DropTable(
                name: "SanPhamDoiTra");

            migrationBuilder.DropTable(
                name: "ThongTinVanChuyen");

            migrationBuilder.DropTable(
                name: "YeuCauDoiTra");

            migrationBuilder.DropIndex(
                name: "IX_DonHang_ID_MaGiamGia",
                table: "DonHang");

            migrationBuilder.DropIndex(
                name: "IX_DonHang_ID_PhuongThucThanhToan",
                table: "DonHang");

            migrationBuilder.DropIndex(
                name: "IX_DonHang_ID_ThongTinVanChuyen",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "ID_MaGiamGia",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "ID_PhuongThucThanhToan",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "ID_ThongTinVanChuyen",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "phiVanChuyen",
                table: "DonHang");

            migrationBuilder.RenameColumn(
                name: "ghiChu",
                table: "DonHang",
                newName: "phuongThucThanhToan");

            migrationBuilder.AddColumn<string>(
                name: "diaChiVanChuyen",
                table: "DonHang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
