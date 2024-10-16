using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.ViewModel.Emp
{
    public record DelEmpViewModel
    {
        [Required]
        public string EmpId { get; set; }
    }
}
