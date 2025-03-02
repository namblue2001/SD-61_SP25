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
            migrationBuilder.CreateTable(
                name: "ChatLieu",
                columns: table => new
                {
                    ID_ChatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenChatLieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLieu", x => x.ID_ChatLieu);
                });

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    ID_DanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenDanhMuc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.ID_DanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "KichThuoc",
                columns: table => new
                {
                    ID_KichThuoc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenKichThuoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KichThuoc", x => x.ID_KichThuoc);
                });

            migrationBuilder.CreateTable(
                name: "MauSac",
                columns: table => new
                {
                    ID_MauSac = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenMauSac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSac", x => x.ID_MauSac);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    ID_SanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    giaGoc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.ID_SanPham);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    ID_TaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    taiKhoan = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    matKhau = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    soDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    anhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenVaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.ID_TaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieu",
                columns: table => new
                {
                    ID_ThuongHieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenThuongHieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieu", x => x.ID_ThuongHieu);
                });

            migrationBuilder.CreateTable(
                name: "XuatXu",
                columns: table => new
                {
                    ID_XuatXu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenXuatXu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatXu", x => x.ID_XuatXu);
                });

            migrationBuilder.CreateTable(
                name: "DiaChi",
                columns: table => new
                {
                    ID_DiaChi = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_TaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    soNha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    xa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    huyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tinhThanhPho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaChi", x => x.ID_DiaChi);
                    table.ForeignKey(
                        name: "FK_DiaChi_TaiKhoan_ID_TaiKhoan",
                        column: x => x.ID_TaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "ID_TaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamChiTiet",
                columns: table => new
                {
                    ID_SanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_SanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Mau = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_XuatXu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_ThuongHieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Size = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_DanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_ChatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamChiTiet", x => x.ID_SanPhamChiTiet);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_ChatLieu_ID_ChatLieu",
                        column: x => x.ID_ChatLieu,
                        principalTable: "ChatLieu",
                        principalColumn: "ID_ChatLieu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_DanhMuc_ID_DanhMuc",
                        column: x => x.ID_DanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "ID_DanhMuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_KichThuoc_ID_Size",
                        column: x => x.ID_Size,
                        principalTable: "KichThuoc",
                        principalColumn: "ID_KichThuoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_MauSac_ID_Mau",
                        column: x => x.ID_Mau,
                        principalTable: "MauSac",
                        principalColumn: "ID_MauSac",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_SanPham_ID_SanPham",
                        column: x => x.ID_SanPham,
                        principalTable: "SanPham",
                        principalColumn: "ID_SanPham",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_ThuongHieu_ID_ThuongHieu",
                        column: x => x.ID_ThuongHieu,
                        principalTable: "ThuongHieu",
                        principalColumn: "ID_ThuongHieu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_XuatXu_ID_XuatXu",
                        column: x => x.ID_XuatXu,
                        principalTable: "XuatXu",
                        principalColumn: "ID_XuatXu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HinhAnh",
                columns: table => new
                {
                    ID_HinhAnh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_SanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_SanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    url_hinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhAnh", x => x.ID_HinhAnh);
                    table.ForeignKey(
                        name: "FK_HinhAnh_SanPhamChiTiet_ID_SanPhamChiTiet",
                        column: x => x.ID_SanPhamChiTiet,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID_SanPhamChiTiet");
                    table.ForeignKey(
                        name: "FK_HinhAnh_SanPham_ID_SanPham",
                        column: x => x.ID_SanPham,
                        principalTable: "SanPham",
                        principalColumn: "ID_SanPham");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiaChi_ID_TaiKhoan",
                table: "DiaChi",
                column: "ID_TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnh_ID_SanPham",
                table: "HinhAnh",
                column: "ID_SanPham");

            migrationBuilder.CreateIndex(
                name: "IX_HinhAnh_ID_SanPhamChiTiet",
                table: "HinhAnh",
                column: "ID_SanPhamChiTiet");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaChi");

            migrationBuilder.DropTable(
                name: "HinhAnh");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "SanPhamChiTiet");

            migrationBuilder.DropTable(
                name: "ChatLieu");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "KichThuoc");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "ThuongHieu");

            migrationBuilder.DropTable(
                name: "XuatXu");
        }
    }
}
