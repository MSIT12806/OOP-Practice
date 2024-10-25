
namespace PaymentSystem.Models
{
    public abstract class Emp
    {
        protected IEmpRepository _repository;
        public string Id { get; }
        public string Name { get; private set; }
        public string Address { get; private set; }


        public Emp(string id, IEmpRepository repository)
        {
            this.Id = id;
            this._repository = repository;
        }

        public void InitialData(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }


        public void Update(string name, string address)
        {
            this.Name = name;
            this.Address = address;
            _repository.Update(this);
        }

        public void UpdateName(string name)
        {
            this.Name = name;
            _repository.Update(this);
        }

        public void UpdateAddress(string address)
        {
            this.Address = address;
            _repository.Update(this);
        }

        public abstract Payment Settle();


        public enum PayWayEnum
        {
            Monthly,
            Sales,
            Union,
            Hourly
        }

    }
}
