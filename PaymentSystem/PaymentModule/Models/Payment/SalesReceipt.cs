using System;

namespace Payment.Models.Payment
{
    public class SalesReceipt
    {
        public string Id { get; set; }
        public string EmpId { get; set; }
        public DateTime SalesDate { get; set; }
        public int Commission { get; set; }
    }
}
