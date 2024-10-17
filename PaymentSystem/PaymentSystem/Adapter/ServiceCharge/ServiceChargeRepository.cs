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
            _context = context;
        }
        public void SetServiceCharge(ServiceChargeCore serviceCharge)
        {
            ServiceChargeDbModel serviceChargeDbModel = ToDbModel(serviceCharge);

            if (_context.ServiceCharges.Any(x => x.EmpId == serviceCharge.EmpId && x.MemberId == serviceCharge.MemberId))
            {
                Update(serviceChargeDbModel);
                return;
            }
            else
            {
                Insert(serviceChargeDbModel);
            }
        }

        private void Insert(ServiceChargeDbModel serviceChargeDbModel)
        {
            _context.ServiceCharges.Add(serviceChargeDbModel);
            _context.SaveChanges();
        }

        private void Update(ServiceChargeDbModel serviceChargeDbModel)
        {
            ServiceChargeDbModel serviceChargeDbModelToUpdate = _context.ServiceCharges.First(x => x.EmpId == serviceChargeDbModel.EmpId && x.MemberId == serviceChargeDbModel.MemberId);
            serviceChargeDbModelToUpdate.Amount = serviceChargeDbModel.Amount;
            _context.ServiceCharges.Update(serviceChargeDbModelToUpdate);
            _context.SaveChanges();
        }

        public ServiceChargeDbModel ToDbModel(ServiceChargeCore serviceCharge)
        {
            return new ServiceChargeDbModel
            {
                EmpId = serviceCharge.EmpId,
                MemberId = serviceCharge.MemberId,
                Amount = serviceCharge.Amount
            };
        }

        public ServiceChargeCore ToCoreModel(ServiceChargeDbModel serviceChargeDbModel)
        {
            return new ServiceChargeCore
            {
                EmpId = serviceChargeDbModel.EmpId,
                MemberId = serviceChargeDbModel.MemberId,
                Amount = serviceChargeDbModel.Amount
            };
        }
    }
}
