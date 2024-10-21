namespace PaymentSystem.Adapter
{
    public class SalesReceiptQueryPage
    {
        public SalesReceiptQueryPage(string empId, List<SalesReceiptQueryViewModel> salesReceipts)
        {
            this.EmpId = empId;
            this.SalesReceipts = salesReceipts;
        }

        public string EmpId { get; set; }
        public List<SalesReceiptQueryViewModel> SalesReceipts { get; set; }
    }
}
