using System;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
	public class VaiTro
	{
		[Key]
		public Guid ID_VaiTro { get; set; }

        [Required(ErrorMessage = "ID tài khoản là bắt buộc.")]
        public Guid ID_TaiKhoan { get; set;  }

        [Required(ErrorMessage = "Tên vai trò là bắt buộc.")]
        public string tenVaiTro { get; set; }

		public TaiKhoan TaiKhoan { get; set; }
	}
}

