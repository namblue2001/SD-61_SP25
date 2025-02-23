using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tài khoản là bắt buộc.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Tài khoản phải có độ dài từ 5 đến 20 ký tự.")]
        public string tenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có độ dài tối thiểu 8 ký tự và tối đa 20 ký tự.")]
        [DataType(DataType.Password)]
        public string matKhau { get; set; }
       
        public Guid ID_VaiTro { get; set; } 
    }
}
