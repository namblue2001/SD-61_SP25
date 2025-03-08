using System;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models.PhieuGiamGiaVaKhuyenMai
{
	public class PhieuGiamGia
	{
        [Key]
        public Guid Ma_PhieuGiamGia { get; set; }

        [Required(ErrorMessage = "Tên phiếu giảm giá là bắt buộc.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên phiếu giảm giá phải từ 3 đến 100 ký tự.")]
        [RegularExpression(@"^\S(.*\S)?$", ErrorMessage = "Tên phiếu giảm giá không được có khoảng trắng ở đầu và cuối.")]
        public string TenPhieuGiamGia { get; set; }

        [Required(ErrorMessage = "Mô tả là bắt buộc.")]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Mô tả phải từ 10 đến 300 ký tự.")]
        [RegularExpression(@"^\S(.*\S)?$", ErrorMessage = "Mô tả phiếu giảm giá không được có khoảng trắng ở đầu và cuối.")]
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Loại giảm giá là bắt buộc.")]
        [RegularExpression(@"^(Phần trăm|Số tiền cố định|Miễn phí giao hàng)$", ErrorMessage = "Loại giảm giá chỉ có thể là 'Phần trăm', 'Số tiền cố định' hoặc 'Miễn phí giao hàng'.")]
        public string LoaiGiamGia { get; set; }

        [Required(ErrorMessage = "Giá trị khuyến mãi là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá trị khuyến mãi phải lớn hơn 0.")]
        [RegularExpression(@"^(?! )[0-9]+(?! )$", ErrorMessage = "Giá trị khuyến mãi phải là số và không được có khoảng trắng ở đầu và cuối.")]
        public double GiaTriKhuyenMai { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
        public DateTime NgayBatDau { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Ngày hết hạn là bắt buộc.")]
        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(PhieuGiamGiaValidator), nameof(PhieuGiamGiaValidator.ValidateNgayHetHan))]
        public DateTime NgayHetHan { get; set; }

        [Required(ErrorMessage = "Số lượng tổng là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng tổng phải lớn hơn 0.")]
        [RegularExpression(@"^(?! )[0-9]+(?! )$", ErrorMessage = "Số lượng tổng phải là số và không được có khoảng trắng ở đầu và cuối.")]
        public int SoLuongTong { get; set; }

        [Required(ErrorMessage = "Số lượng tối đa cho 1 người là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng tối đa cho 1 người phải lớn hơn 0.")]
        [RegularExpression(@"^(?! )[0-9]+(?! )$", ErrorMessage = "Số lượng tối đa cho 1 người phải là số và không được có khoảng trắng ở đầu và cuối.")]
        public int SoLuongToiDaCho1Nguoi { get; set; }

        [Required(ErrorMessage = "Giá trị đơn hàng tối thiểu là bắt buộc.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá trị đơn hàng tối thiểu phải lớn hơn 0.")]
        [RegularExpression(@"^(?! )[0-9]+(?! )$", ErrorMessage = "Giá trị đơn hàng tối thiểu phải là số và không được có khoảng trắng ở đầu và cuối.")]
        public double GiaTriDonHangToiThieu { get; set; }
    }
    public static class PhieuGiamGiaValidator
    {
        public static ValidationResult ValidateNgayHetHan(DateTime ngayHetHan, ValidationContext context)
        {
            if (ngayHetHan <= DateTime.Now)
            {
                return new ValidationResult("Ngày hết hạn phải lớn hơn ngày hiện tại.");
            }
            return ValidationResult.Success;
        }
    }
}

