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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                    gioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenVaiTro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.ID_TaiKhoan);
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
                name: "ThuongHieu",
                columns: table => new
                {
                    ID_ThuongHieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenThuongHieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false),
                    anhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    ID_SanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_DanhMuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenSanPham = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    giaGoc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    anhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.ID_SanPham);
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMuc_ID_DanhMuc",
                        column: x => x.ID_DanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "ID_DanhMuc",
                        onDelete: ReferentialAction.Cascade);
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
                name: "DonHang",
                columns: table => new
                {
                    ID_DonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_TaiKhoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ngayDatHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ID_ThongTinVanChuyen = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_MaGiamGia = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    phiVanChuyen = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trangThaiDonHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trangThaiThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phuongThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ghiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.ID_DonHang);
                    table.ForeignKey(
                        name: "FK_DonHang_PhieuGiamGia_ID_MaGiamGia",
                        column: x => x.ID_MaGiamGia,
                        principalTable: "PhieuGiamGia",
                        principalColumn: "Ma_PhieuGiamGia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHang_TaiKhoan_ID_TaiKhoan",
                        column: x => x.ID_TaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "ID_TaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonHang_ThongTinVanChuyen_ID_ThongTinVanChuyen",
                        column: x => x.ID_ThongTinVanChuyen,
                        principalTable: "ThongTinVanChuyen",
                        principalColumn: "ID_VanChuyen",
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
                    ID_KieuDang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_ChatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    giaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    anhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    soLuongTon = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_SanPhamChiTiet_KichThuoc_ID_Size",
                        column: x => x.ID_Size,
                        principalTable: "KichThuoc",
                        principalColumn: "ID_KichThuoc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_KieuDang_ID_KieuDang",
                        column: x => x.ID_KieuDang,
                        principalTable: "KieuDang",
                        principalColumn: "ID_KieuDang",
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
                name: "LichSuDonHang",
                columns: table => new
                {
                    ID_LichSuDonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_DonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    trangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayDatHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ghiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuDonHang", x => x.ID_LichSuDonHang);
                    table.ForeignKey(
                        name: "FK_LichSuDonHang_DonHang_ID_DonHang",
                        column: x => x.ID_DonHang,
                        principalTable: "DonHang",
                        principalColumn: "ID_DonHang",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ChiTietDonHang",
                columns: table => new
                {
                    ID_DonHangChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_DonHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_SanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    soLuong = table.Column<int>(type: "int", nullable: false),
                    donGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => x.ID_DonHangChiTiet);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHang_ID_DonHang",
                        column: x => x.ID_DonHang,
                        principalTable: "DonHang",
                        principalColumn: "ID_DonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_SanPhamChiTiet_ID_SanPhamChiTiet",
                        column: x => x.ID_SanPhamChiTiet,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID_SanPhamChiTiet",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "HinhAnh",
                columns: table => new
                {
                    ID_HinhAnh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                        principalColumn: "ID_SanPhamChiTiet",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    ID_KhuyenMai = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TyLeKhuyenMai = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_SanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMai", x => x.ID_KhuyenMai);
                    table.ForeignKey(
                        name: "FK_KhuyenMai_SanPhamChiTiet_ID_SanPhamChiTiet",
                        column: x => x.ID_SanPhamChiTiet,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID_SanPhamChiTiet",
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
                        principalColumn: "ID_DonHangChiTiet");
                    table.ForeignKey(
                        name: "FK_SanPhamDoiTra_YeuCauDoiTra_ID_YeuCauDoiTra",
                        column: x => x.ID_YeuCauDoiTra,
                        principalTable: "YeuCauDoiTra",
                        principalColumn: "ID_YeuCauDoiTra");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnhMinhChung_ID_YeuCauDoiTra",
                table: "AnhMinhChung",
                column: "ID_YeuCauDoiTra");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_ID_DonHang",
                table: "ChiTietDonHang",
                column: "ID_DonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_ID_SanPhamChiTiet",
                table: "ChiTietDonHang",
                column: "ID_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_DiaChi_ID_TaiKhoan",
                table: "DiaChi",
                column: "ID_TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_ID_MaGiamGia",
                table: "DonHang",
                column: "ID_MaGiamGia");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_ID_TaiKhoan",
                table: "DonHang",
                column: "ID_TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_ID_ThongTinVanChuyen",
                table: "DonHang",
                column: "ID_ThongTinVanChuyen");

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
                name: "IX_HinhAnh_ID_SanPhamChiTiet",
                table: "HinhAnh",
                column: "ID_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_ID_DonHang",
                table: "HoaDon",
                column: "ID_DonHang");

            migrationBuilder.CreateIndex(
                name: "IX_KhuyenMai_ID_SanPhamChiTiet",
                table: "KhuyenMai",
                column: "ID_SanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "UQ_TyLeKhuyenMai",
                table: "KhuyenMai",
                columns: new[] { "TyLeKhuyenMai", "NgayBatDau" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LichSuDonHang_ID_DonHang",
                table: "LichSuDonHang",
                column: "ID_DonHang");

            migrationBuilder.CreateIndex(
                name: "UQ_TenPhieuGiamGia",
                table: "PhieuGiamGia",
                column: "TenPhieuGiamGia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_ID_DanhMuc",
                table: "SanPham",
                column: "ID_DanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ID_ChatLieu",
                table: "SanPhamChiTiet",
                column: "ID_ChatLieu");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_ID_KieuDang",
                table: "SanPhamChiTiet",
                column: "ID_KieuDang");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnhMinhChung");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DiaChi");

            migrationBuilder.DropTable(
                name: "GioHangChiTiet");

            migrationBuilder.DropTable(
                name: "HinhAnh");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "LichSuDonHang");

            migrationBuilder.DropTable(
                name: "SanPhamDoiTra");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "YeuCauDoiTra");

            migrationBuilder.DropTable(
                name: "SanPhamChiTiet");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "ChatLieu");

            migrationBuilder.DropTable(
                name: "KichThuoc");

            migrationBuilder.DropTable(
                name: "KieuDang");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "ThuongHieu");

            migrationBuilder.DropTable(
                name: "XuatXu");

            migrationBuilder.DropTable(
                name: "PhieuGiamGia");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "ThongTinVanChuyen");

            migrationBuilder.DropTable(
                name: "DanhMuc");
        }
    }
}
