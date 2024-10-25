




using System.Configuration;

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
    }
    public class MounthlyEmployee : Emp
    {
        public MounthlyEmployee(string id, IEmpRepository repository) : base(id, repository)
        {
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

        public override Payment Settle()
        {
            var salary = _repository.GetSalary(this.Id);

            return new Payment
            {
                EmpId = this.Id,
                Salary = salary.Amount,
            };
        }
    }
    public class UnionEmployee : MounthlyEmployee
    {
        public UnionEmployee(string id, IEmpRepository repository) : base(id, repository)
        {
        }

        public IEnumerable<ServiceChargeCore> ServiceCharges => _repository.GetServiceCharges(this.Id);

        public string SubmitServiceCharge(string id, int amount, DateOnly dateOnly)
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

        public void WithdrawServiceCharge(string setviceChargeId)
        {
            _repository.DeleteServiceChargeBy(setviceChargeId);
        }

        public IEnumerable<ServiceChargeCore> GetServiceCharge()
        {
            return _repository.GetServiceCharges(this.Id);
        }

        public override Payment Settle()
        {
            var salary = _repository.GetSalary(this.Id);

            return new Payment
            {
                EmpId = this.Id,
                Salary = salary.Amount - ServiceCharges.Sum(i=>i.Amount),
            };
        }
    }
    public class SalesEmployee : MounthlyEmployee
    {
        public SalesEmployee(string id, IEmpRepository repository) : base(id, repository)
        {
        }

        public IEnumerable<SalesReceiptCore> SalesReceipts => _repository.GetSalesReceipts(this.Id);

        public string AddSalesReceipt(string id, DateOnly dateOnly, int commission)
        {
            var salesReceipt = new SalesReceiptCore
            {
                EmpId = id,
                SalesDate = dateOnly,
                Commission = commission
            };

            return this._repository.AddSalesReceipt(salesReceipt);
        }

        public IEnumerable<SalesReceiptCore> GetSalesReceipts()
        {
            return this._repository.GetSalesReceipts(this.Id);
        }

        public void DeleteSalesReceiptBy(string salesReceiptId)
        {
            this._repository.DeleteSalesReceiptBy(salesReceiptId);
        }
    }
}
