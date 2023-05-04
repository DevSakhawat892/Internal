using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Emon
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Emon
 */
namespace TUSO.Infrastructure.Repositories
{
    public class SLARepository : Repository<SLA>, ISLARepository
    {
        public SLARepository(DataContext context) : base(context)
        {
        }

        public async Task<SLA> GetSlaByKey(long OID)
        {
            try
            {
                return await FirstOrDefaultAsync(s => s.OID == OID && s.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SLA>> ReadSLAs()
        {
            try
            {
                return await QueryAsync(s => s.IsDeleted == false, i => i.Holidays);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}