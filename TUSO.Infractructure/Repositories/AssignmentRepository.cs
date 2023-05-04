using TUSO.Domain.Entities;
using TUSO.Infrastructure.Contracts;
using TUSO.Infrastructure.SqlServer;

/*
 * Created by: Bithy
 * Date created: 05.09.2022
 * Last modified: 10.09.2022
 * Modified by: Bithy
 */
namespace TUSO.Infrastructure.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(DataContext context) : base(context)
        {

        }

        public async Task<Assignment> GetAssignmentByKey(long OID)
        {
            try
            {
                return await FirstOrDefaultAsync(a => a.OID == OID && a.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Assignment>> GetAssignments()
        {
            try
            {
                return await QueryAsync(u => u.IsDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}