using System;

namespace Payment.Models.Payment
{
    public class TimeCard
    {
        const int OVERTIME_THRESHOLD = 8;
        public string EmpId { get; set; }
        public DateTime WorkDate { get; set; }
        public int Hours { get; set; }
        public int GetRegularHours()
        {
            return Hours > OVERTIME_THRESHOLD ? OVERTIME_THRESHOLD : Hours;
        }
        public int GetOvertimeHours()
        {
            return Hours > OVERTIME_THRESHOLD ? Hours - OVERTIME_THRESHOLD : 0;
        }
    }
}
