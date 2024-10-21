﻿using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Adapter
{
    public class SalesReceiptAddViewModel
    {
        [Required]
        public string EmpId { get; set; }

        [Required]
        public DateTime SalesDate { get; set; }

        [Required]
        public int Commission { get; set; }
    }
}