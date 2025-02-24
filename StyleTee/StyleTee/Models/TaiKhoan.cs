using System;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
	public class TaiKhoan
	{
		[Key]
        [Required(ErrorMessage = "ID tài khoản là bắt buộc.")]
        public Guid ID_TaiKhoan { get; set; }

        [Required(ErrorMessage = "Tài khoản là bắt buộc.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Tài khoản phải có độ dài từ 5 đến 20 ký tự.")]
        public string taiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có độ dài tối thiểu 8 ký tự và tối đa 20 ký tự.")]
        [DataType(DataType.Password)]
        public string matKhau { get; set; }

        [EmailAddress(ErrorMessage = "E-mail không hợp lệ.")]
        [Required(ErrorMessage = "E-mail là bắt buộc.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc.")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "Họ tên phải có độ dài tối thiểu 7 ký tự và tối đa 50 ký tự.")]
        public string hoTen { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string soDienThoai { get; set; }

		public string? anhDaiDien { get; set; }

        [RegularExpression("^(Nam|Nu)$", ErrorMessage = "Giới tính chỉ có thể là 'Nam' hoặc 'Nữ'.")]
        public string? gioiTinh { get; set; }
        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        public DateTime ngaySinh { get; set; }

        [Required(ErrorMessage = "Trạn thái là bắt buộc.")]
        [RegularExpression("^(Hoạt động|Ngừng hoạt động)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public string trangThai { get; set; }

        [Required(ErrorMessage = "Tên vai trò là bắt buộc.")]
        [RegularExpression("^(Quản lý|Nhân viên|Khách hàng)$", ErrorMessage = "Vai trò chỉ có thể là Quản lý , Nhân viên hay Khách hàng .")]
        public string tenVaiTro { get; set; }

    }
}

