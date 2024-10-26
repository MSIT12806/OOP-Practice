using PaymentSystem.Models;

namespace PaymentSystem.Application.Payday
{
    public interface IServiceChargeSetter
    {
        void SetServiceCharge(IEnumerable<Payroll> paydays);
    }

}
