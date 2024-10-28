using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Adapter.Payment
{
    public class  SalesReceiptQueryViewModel
    {
        public string Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SalesDate { get; set; }
        public int Commission { get; set; }
    }
}
