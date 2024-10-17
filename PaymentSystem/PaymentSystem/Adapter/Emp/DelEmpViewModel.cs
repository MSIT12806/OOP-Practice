using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Adapter
{
    public record DelEmpViewModel
    {
        [Required]
        public string EmpId { get; set; }
    }
}
