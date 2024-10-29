namespace PaymentSystem.Infrastructure.ORM
{
    public class CompensationAlterEventDbModel
    {
        public string EmpId { get; internal set; }
        public int Amount { get; internal set; }
        public DateOnly StartDate { get; internal set; }
        public string EmployeeType { get; internal set; }
        public DateTime CreateDateTime { get; internal set; }
    }
}
