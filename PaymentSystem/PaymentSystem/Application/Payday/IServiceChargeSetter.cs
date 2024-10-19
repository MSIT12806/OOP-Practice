namespace PaymentSystem.Application.Payday
{
    public interface IServiceChargeSetter
    {
        void SetServiceCharge(IEnumerable<Models.PaydayCore> paydays);
    }

}
