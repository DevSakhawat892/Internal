using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Sakhawat
 * Date created: 05.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class IncidentStatusRepository : Repository<IncidentStatus>, IIncidentStatusRepository
    {
        public IncidentStatusRepository(DataContext context) : base(context)
        {
        }

        public async Task<IncidentStatus> GetIncidentStatusByKey(long OID)
        {
            try
            {
                return await FirstOrDefaultAsync(i => i.OID == OID && i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IncidentStatus> GetIncidentStatusByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(i => i.Comment.ToLower().Trim() == name.ToLower().Trim() && i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<IncidentStatus>> GetIncidentStatuses()
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