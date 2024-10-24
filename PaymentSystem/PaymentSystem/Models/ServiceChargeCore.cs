﻿namespace PaymentSystem.Models
{
    public class ServiceChargeCore
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public int Amount { get; set; }
        public DateOnly ApplyDate { get; set; }
        public DateOnly ExpectedPayDate { get; set; }
        public bool Paid { get; set; }
    }
}
