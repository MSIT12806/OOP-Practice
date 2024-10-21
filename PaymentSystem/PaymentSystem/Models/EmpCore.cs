using PaymentSystem.Domain.interfaces;

namespace PaymentSystem.Models
{
    public class EmpCore
    {
        public string Id { get; }
        public string Name { get; set; }
        public string Address { get; set; }
        public EmpCore(string id)
        {
            this.Id = id;
        }

        public void InjectData(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }

        public void InjectDataFromDB(IEmpRepository repository)
        {
            repository.InjectData(this);
        }

        public void Update(IEmpRepository repository)
        {
            repository.Update(this);
        }
    }
}
