using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Sakhawat
 * Date created: 04.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class IncidentTypeRepository : Repository<IncidentType>, IIncidentTypeRepository
    {
        public IncidentTypeRepository(DataContext context) : base(context)
        {

        }

        public async Task<IncidentType> GetIncidentTypeByKey(int OID)
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

        public async Task<IncidentType> GetIncidentTypeByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(i => i.IncidentTypeName.ToLower().Trim() == name.ToLower().Trim() && i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<IncidentType>> GetIncidentTypes()
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