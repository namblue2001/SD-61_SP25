using System;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
	public class DiaChi
	{
		[Key]
		public Guid ID_DiaChi { get; set; }

        [Required(ErrorMessage = "ID tài khoản là bắt buộc.")]
        public Guid ID_TaiKhoan { get; set; }

        [Required(ErrorMessage = "Tên người nhận là bắt buộc.")]
        public string tenNguoiNhan { get; set; }

        [Required(ErrorMessage = "Số điện thoại người nhận là bắt buộc.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và có đúng 10 chữ số.")]
        public string sdtNguoiNhan { get; set; }

        [Required(ErrorMessage = "Số nhà là bắt buộc.")]
        public string soNha { get; set; }

        [Required(ErrorMessage = "Xã là bắt buộc.")]
        public string xa { get; set; }

        [Required(ErrorMessage = "Huyện là bắt buộc.")]
        public string huyen { get; set; }

        [Required(ErrorMessage = "Tỉnh/Thành phố là bắt buộc.")]
        public string tinhThanhPho { get; set; }

        [RegularExpression("^(Hoạt động|Ngừng hoạt động)$", ErrorMessage = "Trạng thái chỉ có thể là 'Hoạt động' hoặc 'Ngừng hoạt động'.")]
        public string trangThai { get; set; }

		public TaiKhoan TaiKhoan { get; set; }
	}
}

