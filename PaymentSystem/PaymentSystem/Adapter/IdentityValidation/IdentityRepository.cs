using IdentityCoreModule.Domain;
using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Adapter.IdentityValidation
{

    public class HRForId : Identity
    {
        public HRForId(string userName)
        {
            this.UserName = userName;
        }
        public string UserName { get; set; }
    }
    public class HRPolicy : GetIdentityterRolePolicy<HRForId>
    {
        protected override string RoleName => nameof(HRForId);
    }
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class EmpForId : Identity
    {
        public EmpForId(string userName)
        {
            this.UserName = userName;
        }
        public string UserName { get; set; }
    }
}
