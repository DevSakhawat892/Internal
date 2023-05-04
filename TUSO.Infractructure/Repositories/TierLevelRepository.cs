using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Sakhawat
 * Date created: 24.09.2022
 * Last modified: 24.09.2022
 * Modified by: Sakhawat
 */
namespace TUSO.Infrastructure.Repositories
{
   public class TierLevelRepository : Repository<TierLevel>, ITierLevelRepository
   {
      public TierLevelRepository(DataContext context) : base(context)
      {
      }

      public async Task<TierLevel> GetTierLevelByKey(int OID)
      {
         try
         {
            return await FirstOrDefaultAsync(t => t.OID == OID && t.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<TierLevel> GetTierLevelByName(string name)
      {
         try
         {
            return await FirstOrDefaultAsync(t => t.TierName == name && t.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<TierLevel>> GetTierLevels()
      {
         try
         {
            return await QueryAsync(w => w.IsDeleted == false, i => i.Departments, p => p.IncidentTypes);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}