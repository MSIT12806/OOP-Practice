using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Adapter.BasicDataMaintenence
{
    public record DelEmpViewModel
    {
        [Required]
        public string EmpId { get; set; }
    }
}
