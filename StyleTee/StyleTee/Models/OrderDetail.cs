using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal Subtotal { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
} 