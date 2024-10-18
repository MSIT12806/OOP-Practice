using PaymentSystem.Application.Emp;
using PaymentSystem.Infrastructure.ORM;
using PaymentSystem.Models;

namespace PaymentSystem.Adapter
{
    public class ServiceChargeRepository : IServiceChargeRepository
    {
        private AppDbContext _context;

        public ServiceChargeRepository(AppDbContext context)
        {
            this._context = context;
        }
        public void SetServiceCharge(ServiceChargeCore serviceCharge)
        {
            ServiceChargeDbModel serviceChargeDbModel = this.ToDbModel(serviceCharge);

            if (this._context.ServiceCharges.Any(x => x.EmpId == serviceCharge.EmpId && x.MemberId == serviceCharge.MemberId))
            {
                this.Update(serviceChargeDbModel);
                return;
            }
            else
            {
                this.Insert(serviceChargeDbModel);
            }
        }

        public ServiceChargeDbModel ToDbModel(ServiceChargeCore serviceCharge)
        {
            return new ServiceChargeDbModel
            {
                EmpId = serviceCharge.EmpId,
                MemberId = serviceCharge.MemberId,
                ServiceCharge = serviceCharge.Amount
            };
        }

        public ServiceChargeCore ToCoreModel(ServiceChargeDbModel serviceChargeDbModel)
        {
            return new ServiceChargeCore
            {
                EmpId = serviceChargeDbModel.EmpId,
                MemberId = serviceChargeDbModel.MemberId,
                Amount = serviceChargeDbModel.ServiceCharge
            };
        }

        public IEnumerable<ServiceChargeCore> GetServiceCharges()
        {
            var result = this._context.ServiceCharges.ToList().Select(this.ToCoreModel);
            return result;
        }



        private void Insert(ServiceChargeDbModel serviceChargeDbModel)
        {
            this._context.ServiceCharges.Add(serviceChargeDbModel);
            this._context.SaveChanges();
        }

        private void Update(ServiceChargeDbModel serviceChargeDbModel)
        {
            ServiceChargeDbModel serviceChargeDbModelToUpdate = this._context.ServiceCharges.First(x => x.EmpId == serviceChargeDbModel.EmpId && x.MemberId == serviceChargeDbModel.MemberId);
            serviceChargeDbModelToUpdate.ServiceCharge = serviceChargeDbModel.ServiceCharge;
            this._context.ServiceCharges.Update(serviceChargeDbModelToUpdate);
            this._context.SaveChanges();
        }
    }
}
