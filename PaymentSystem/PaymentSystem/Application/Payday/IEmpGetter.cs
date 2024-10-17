namespace PaymentSystem.Application.Payday
{
    public interface IEmpGetter
    {
        IEnumerable<EmpForPay> GetEmps();
    }

}
