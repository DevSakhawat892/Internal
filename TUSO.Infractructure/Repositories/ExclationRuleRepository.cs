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
    public class ExclationRuleRepository : Repository<ExclationRule>, IExclationRuleRepository
    {
        public ExclationRuleRepository(DataContext context) : base(context)
        {

        }

        public async Task<ExclationRule> GetExclationRuleByKey(int OID)
        {
            try
            {
                return await FirstOrDefaultAsync(c => c.OID == OID && c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ExclationRule>> GetExclationRules()
        {
            try
            {
                return await QueryAsync(c => c.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}