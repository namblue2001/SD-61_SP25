using System;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
	public class TaiKhoan
	{
		[Key]
        
        public Guid ID_TaiKhoan { get; set; }

        [Required(ErrorMessage = "Tài khoản là bắt buộc.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Tài khoản phải có độ dài từ 5 đến 20 ký tự.")]
        public string taiKhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có độ dài tối thiểu 8 ký tự và tối đa 20 ký tự.")]
        [DataType(DataType.Password)]
        public string matKhau { get; set; }

        [EmailAddress(ErrorMessage = "E-mail không hợp lệ.")]
        //[Required(ErrorMessage = "E-mail là bắt buộc.")]
        public string? email { get; set; }

		public string ?hoTen { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string ?soDienThoai { get; set; }

        //[RegularExpression("^(Nam|Nu)$", ErrorMessage = "Giới tính chỉ có thể là 'Nam' hoặc 'Nữ'.")]
        public string ?gioiTinh { get; set; }

		public DateTime? ngaySinh { get; set; }

        //[RegularExpression("^(Hoạt động|Ngừng hoạt động)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public string ?trangThai { get; set; }

        public Guid ID_VaiTro { get; set; }

        public VaiTro VaiTro { get; set; }
    }
}

