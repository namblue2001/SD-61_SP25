using System;
using System.ComponentModel.DataAnnotations;

namespace StyleTee.Models
{
    public class OrderStatusHistory
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime StatusDate { get; set; }

        public string Notes { get; set; }

        // Navigation property
        public virtual Order Order { get; set; }
    }
} 