﻿using System;

namespace Payment.Models.Payment
{
    public class ServiceCharge
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public int Amount { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime ExpectedPayDate { get; set; }
        public bool Paid { get; set; }
    }
}
