using System.ComponentModel.DataAnnotations;
using static Payment.Models.BasicDataMaintenece.Employee;

namespace PaymentSystem.Adapter.BasicDataMaintenence
{
    public record AddEmpViewModel
    {
        [Required]
        public string EmpId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public PayWayEnum PayWay { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }
    }
}
