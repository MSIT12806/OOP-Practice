using System.ComponentModel;

namespace PaymentSystem.Adapter.Payday
{
    public class TimeCardSaveViewModel : TimeCardViewModel
    {
        [ReadOnly(true)]
        public new string EmpId { get; }
    }
}
