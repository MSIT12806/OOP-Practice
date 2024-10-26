using PaymentSystem.Application.ServiceCharge;
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
        public string AddServiceCharge(ServiceChargeCore serviceCharge)
        {
            ServiceChargeDbModel serviceChargeDbModel = this.ToDbModel(serviceCharge);
            this.Insert(serviceChargeDbModel);

            return serviceChargeDbModel.ServiceChargeId;
        }

        public void DeleteServiceCharge(string serviceChargeId)
        {
            ServiceChargeDbModel serviceChargeDbModel = this._context.ServiceCharges.First(x => x.ServiceChargeId == serviceChargeId);
            this._context.ServiceCharges.Remove(serviceChargeDbModel);
            this._context.SaveChanges();
        }

        public ServiceChargeDbModel ToDbModel(ServiceChargeCore serviceCharge)
        {
            return new ServiceChargeDbModel
            {
                ServiceChargeId = serviceCharge.Id,
                EmpId = serviceCharge.EmpId,
                ServiceCharge = serviceCharge.Amount,
                ApplyDate = serviceCharge.ApplyDate,
            };
        }

        public ServiceChargeCore ToCoreModel(ServiceChargeDbModel serviceChargeDbModel)
        {
            return new ServiceChargeCore
            {
                EmpId = serviceChargeDbModel.EmpId,
                Id = serviceChargeDbModel.ServiceChargeId,
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
            ServiceChargeDbModel serviceChargeDbModelToUpdate = this._context.ServiceCharges.First(x => x.EmpId == serviceChargeDbModel.EmpId && x.ServiceChargeId == serviceChargeDbModel.ServiceChargeId);
            serviceChargeDbModelToUpdate.ServiceCharge = serviceChargeDbModel.ServiceCharge;
            this._context.ServiceCharges.Update(serviceChargeDbModelToUpdate);
            this._context.SaveChanges();
        }
    }
}
