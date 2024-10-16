using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.ViewModel
{
    public record ServiceChargeViewModel
    {
        [Required]
        [DisplayName("公會會員編號")]
        public string MemberId { get; set; }

        [Required]
        [DisplayName("員工編號")]
        public string EmpId { get; set; }

        [Required]
        [DisplayName("每月扣繳服務費")]
        public string Amount { get; set; }
    }
}
