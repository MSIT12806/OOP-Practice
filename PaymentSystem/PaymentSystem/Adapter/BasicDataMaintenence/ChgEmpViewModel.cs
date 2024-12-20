﻿using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Adapter.BasicDataMaintenence
{
    public record ChgEmpViewModel
    {
        [Required]
        public string EmpId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
