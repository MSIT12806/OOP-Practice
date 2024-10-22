




namespace PaymentSystem.Models
{
    public class EmpCore
    {
        private IEmpRepository _repository;
        public string Id { get; }
        public string Name { get; private set; }
        public string Address { get; private set; }

        public IEnumerable<SalesReceiptCore> SalesReceipts => _repository.GetSalesReceipts(this.Id);
        public IEnumerable<ServiceChargeCore> ServiceCharges => _repository.GetServiceCharges(this.Id);

        public EmpCore(string id, IEmpRepository repository)
        {
            this.Id = id;
            this._repository = repository;
        }

        public void InitialData(string name , string address)
        {
            this.Name = name;
            this.Address = address;
        }

        public void InjectDataFromDB()
        {
            _repository.InjectData(this);
        }

        public void Update(string name, string address)
        {
            this.InitialData(name, address);
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

        public void SetSalary(int amount, EmpSalaryCore.PayWayEnum payWay)
        {
            var salary = new EmpSalaryCore(this.Id, amount, payWay);
            _repository.AddSalary(salary);
        }

        public EmpSalaryCore GetSalary()
        {
            return _repository.GetSalary(this.Id);
        }

        public PaydayCore Pay()
        {
            var salary = _repository.GetSalary(this.Id);
            var salesReceipts = SalesReceipts;
            var serviceCharge = _repository.GetServiceCharges(this.Id);

            return new PaydayCore
            {
                EmpId = this.Id,
                Salary = salary.Amount,
                SalesReceipt = salesReceipts.Sum(x => x.Commission),
                ServiceCharge = serviceCharge.Sum(x => x.Amount)
            };
        }

        public string AddServiceCharge(string id, int amount, DateOnly dateOnly)
        {
            var serviceCharge = new ServiceChargeCore
            {
                EmpId = id,
                Amount = amount,
                ApplyDate = dateOnly,
            };

            return _repository.AddServiceCharge(serviceCharge);
        }

        public ServiceChargeCore GetServiceChargeBy(string setviceChargeId)
        {
            var db = _repository.GetServiceCharges(this.Id).FirstOrDefault(x => x.Id == setviceChargeId);
            return db;
        }

        public void DeleteServiceCharge(string setviceChargeId)
        {
            _repository.DeleteServiceChargeBy(setviceChargeId);
        }

        public IEnumerable<ServiceChargeCore> GetServiceCharge()
        {
            return _repository.GetServiceCharges(this.Id);
        }
    }
}
