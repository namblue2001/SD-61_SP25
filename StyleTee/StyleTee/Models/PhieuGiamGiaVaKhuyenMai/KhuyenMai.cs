using System;
using System.ComponentModel.DataAnnotations;
using StyleTee.Models;

namespace StyleTee.Models.PhieuGiamGiaVaKhuyenMai
{
	public class KhuyenMai
	{
        [Key]
        public Guid ID_KhuyenMai { get; set; }

        [Required(ErrorMessage = "Tỷ lệ khuyến mãi là bắt buộc.")]
        [Range(1, 100, ErrorMessage = "Tỷ lệ khuyến mãi phải từ 1 đến 100.")]
        public int TyLeKhuyenMai { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        [DataType(DataType.DateTime)]
        public DateTime NgayBatDau { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(KhuyenMaiValidator), nameof(KhuyenMaiValidator.ValidateNgayKetThuc))]
        public DateTime NgayKetThuc { get; set; }

	public Guid ID_SanPhamChiTiet { get; set; }

	public SanPhamChiTiet SanPhamChiTiet {get;set;}
    }

    public static class KhuyenMaiValidator
    {
        public static ValidationResult ValidateNgayKetThuc(DateTime ngayKetThuc, ValidationContext context)
        {
            if (ngayKetThuc <= DateTime.Now)
            {
                return new ValidationResult("Ngày kết thúc phải lớn hơn ngày hiện tại.");
            }
            return ValidationResult.Success;
        }
    }
}

