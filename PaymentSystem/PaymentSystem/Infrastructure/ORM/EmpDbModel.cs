using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Infrastructure.ORM
{
    public class EmpDbModel
    {
        [Key]
        public string EmpId { get; internal set; }

        [Required]
        public string Name { get; internal set; }

        [Required]
        public string Address { get; internal set; }
    }
}
