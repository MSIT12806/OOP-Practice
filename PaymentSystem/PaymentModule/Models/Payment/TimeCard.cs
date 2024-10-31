using System;

namespace Payment.Models.Payment
{
    public class TimeCard
    {
        public TimeCard(string empId, DateTime workDate, int hours)
        {
            this.EmpId = empId;
            this.WorkDate = workDate;
            this.Hours = hours;
        }

        const int OVERTIME_THRESHOLD = 8;
        public string EmpId { get; set; }
        public DateTime WorkDate { get; set; }
        public int Hours { get; set; }
        public int GetRegularHours()
        {
            return this.Hours > OVERTIME_THRESHOLD ? OVERTIME_THRESHOLD : this.Hours;
        }
        public int GetOvertimeHours()
        {
            return this.Hours > OVERTIME_THRESHOLD ? this.Hours - OVERTIME_THRESHOLD : 0;
        }
    }
}
