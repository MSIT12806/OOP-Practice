using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace PaymentSystem.Adapter
{
    public record ServiceChargeViewModel
    {
        [Required]
        [DisplayName("員工編號")]
        public string EmpId { get; set; }

        [Required]
        [DisplayName("每月扣繳服務費")]
        public int Amount { get; set; }

        [Required]
        [DisplayName("服務費日期")]
        public DateOnly ChargeDate { get; set; }
    }
}
