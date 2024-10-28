using System.ComponentModel;

namespace PaymentSystem.Adapter.Payment
{
    public class TimeCardSaveViewModel : TimeCardViewModel
    {
        [ReadOnly(true)]
        public new string EmpId { get; }
    }
}
