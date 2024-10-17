namespace PaymentSystem.Application.Payday
{
    public interface IServiceChargeGetter
    {
        IEnumerable<ServiceChargeForPay> GetServiceCharges();
    }

}
