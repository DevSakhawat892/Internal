using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

namespace TUSO.Infrastructure.Repositories
{
   public class IncidentCategoryRepository : Repository<IncidentCategory>, IIncidentCategoryRepository
   {
      public IncidentCategoryRepository(DataContext context) : base(context)
      {
      }

      public async Task<IncidentCategory> GetIncidentCategoryByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(i => i.OID == key && i.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IncidentCategory> GetIncidentCategoryByName(string name)
      {
         try
         {
            return await FirstOrDefaultAsync(i => i.IncidentCategorys.ToLower().Trim() == name.ToLower().Trim() && i.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<IncidentCategory>> GetIncidentCategories()
      {
         try
         {
            return await QueryAsync(i => i.IsDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}