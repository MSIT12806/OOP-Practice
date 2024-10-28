using System;

namespace Payment.Models.Payment
{
    public class TimeCard
    {
        public string EmpId { get; set; }
        public DateTime WorkDate { get; set; }
        public int Hours { get; set; }
    }
}
