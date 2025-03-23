using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string OrderStatus { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        // Navigation properties
        //public virtual ApplicationUser User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }
    }
} 