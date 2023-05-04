using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Bithy
 * Date created: 20.09.2022
 * Last modified: 20.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class PriorityRepository : Repository<Priority>, IPriorityRepository
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="context"></param>
        public PriorityRepository(DataContext context) : base(context)
        {

        }

        public async Task<Priority> GetPriorityByKey(int key)
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

        public async Task<Priority> GetPriorityByName(string name)
        {
            try
            {
                return await FirstOrDefaultAsync(i => i.PriorityName.ToLower().Trim() == name.ToLower().Trim() && i.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Priority>> GetPriorities()
        {
            try
            {
                return await QueryAsync(i => i.IsDeleted == false, i=>i.SLA);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}