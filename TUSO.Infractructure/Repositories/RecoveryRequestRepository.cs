using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

namespace TUSO.Infrastructure.Repositories
{
   public class RecoveryRequestRepository : Repository<RecoveryRequest>, IRecoveryRequestRepository
   {
      public RecoveryRequestRepository(DataContext context):base(context)
      {
      }

      public async Task<RecoveryRequest> GetRecoveryRequestByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(r => r.OID == key && r.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<RecoveryRequest> GetRecoveryRequestByName(string name)
      {
         try
         {
            return await FirstOrDefaultAsync(r=> r.RequestDescription.ToLower().Trim() == name.ToLower().Trim() && r.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<RecoveryRequest>> GetRecoveryRequests()
      {
         try
         {
            //return await QueryAsync(r => r.IsDeleted == false, i => i.UserAccount);
            return await QueryAsync(r => r.IsDeleted == false, i => i.UserAccount);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}
